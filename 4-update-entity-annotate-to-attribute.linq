<Query Kind="Program">
  <NuGetReference>System.Text.Encoding.CodePages</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	string prefix = "FaDMESCloud.EFCore";
	string basePath = $@"D:\WorkSpace\101-Work\WorkCode\FaDMESCloud2022\master-v3.1\WEB\{prefix}";
	string codePath = $@"{basePath}\Table";
	string xmlPath = $@"{basePath}\bin\Debug\netstandard2.0\{prefix}.xml";
	string usingStr = "using System.ComponentModel;";
	string attriFomate = "        [Description(@\"{0}\")]";

	// 获取注释信息
	var csInfos = GetXmlResource(xmlPath, prefix);

	// 获取文件夹下所有cs文件
	var files = GetAllSubFiles(codePath);

	// 遍历文件夹，将文件夹下所有存在注释的cs文件进行注释替换
	foreach (var csInfo in csInfos)
	{
		// if (csInfo.ClassName != "PrintDensity") continue;
		// 获取对应类名的cs文件
		var file = files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f.FullName) == csInfo.ClassName);
		if (file == null)
		{
			Console.WriteLine($"{csInfo.ClassName} 文件获取失败");
			continue;
		}
		var readLines = new List<string>();
		var isHasUsing = false;
		System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
		readLines = File.ReadAllLines(file.FullName, Encoding.GetEncoding("GB2312")).ToList();

		// Console.WriteLine(csInfo.ClassName);
		for (int i = 0; i < readLines.Count; i++)
		{
			var trimL = readLines[i].Trim();
			var replaceL = trimL.Replace(" { get; set; }", "");
			var csPropName = replaceL.Split(" ").LastOrDefault();
			var prop = csInfo.Props.FirstOrDefault(p => p.PropertyName == csPropName);
			if (!isHasUsing && trimL.Trim() == usingStr) isHasUsing = true;
			if (prop != null
			&& trimL.StartsWith("public")
			&& !trimL.StartsWith("public virtual")
			&& csPropName == prop.PropertyName)
			{
				var find = false;
				var t = i - 1;
				while (readLines[t].Trim() != "/// </summary>")
				{
					if (readLines[t].Contains("Description"))
					{
						find = true;
						break;
					}
					t--;
				}
				
				if (!find)
				{
					readLines.Insert(i, string.Format(attriFomate, prop.Comment));
					i += 1;
				}

			}
		}
		if (!isHasUsing) readLines.Insert(0, usingStr);
		// Console.WriteLine(readLines);
		File.WriteAllText(file.FullName, string.Join("\r\n", readLines), Encoding.GetEncoding("GB2312"));
	}

	Console.WriteLine("完成特性添加！！");
}

List<ClassInfo> GetXmlResource(string xmlPath, string prefix)
{
	XmlDocument doc = new XmlDocument();
	doc.Load(xmlPath);
	var memberNodes = doc.SelectNodes("/doc/members/member");
	List<XmlNode> tNodes = new List<System.Xml.XmlNode>();
	List<XmlNode> pNodes = new List<System.Xml.XmlNode>();

	foreach (XmlNode node in memberNodes)
	{
		// 过滤注释
		if (node.NodeType == XmlNodeType.Comment) continue;
		var name = node.Attributes["name"]?.Value;
		if (name.StartsWith("T")) tNodes.Add(node);
		if (name.StartsWith("P")) pNodes.Add(node);
	}

	var csInfos = new List<ClassInfo>();
	foreach (var tn in tNodes)
	{
		var cs = new ClassInfo();
		if (tn.Attributes["name"] == null) continue;
		var tNamespace = tn.Attributes["name"].Value.Split(':')[1];
		cs.ClassName = tNamespace.Split('.').LastOrDefault();
		for (var index = pNodes.Count - 1; index >= 0; index--)
		{
			// xml 接点name属性为空
			if (pNodes[index].Attributes["name"] == null) continue;
			var pns = pNodes[index].Attributes["name"].Value;
			var pNamespace = pns.Split(':')[1];
			var nameSpaceSplit = pNamespace.Split('.');
			var name = nameSpaceSplit.LastOrDefault();
			var pClassName = nameSpaceSplit[nameSpaceSplit.Length - 2];
			// 命名空间不一致
			if (pClassName != cs.ClassName) continue;
			var commentNode = pNodes[index].ChildNodes[0];
			var prop = new PropertyInfo
			{
				PropertyName = name,
				Comment = commentNode.InnerText.Trim(),
			};
			// Console.WriteLine($"{cs.ClassName} {prop.PropertyName} {prop.Comment}");
			cs.Props.Add(prop);
			pNodes.Remove(pNodes[index]);
		}
		if (cs.Props.Any()) csInfos.Add(cs);
	}

	Console.WriteLine($"未匹配属性注释：{string.Join("\r\n", pNodes.Select(n => n.Attributes["name"].Value).ToList())}");

	return csInfos;
}

List<FileInfo> GetAllSubFiles(string direPath)
{

	var files = new List<FileInfo>();
	if (!Directory.Exists(direPath))
	{
		Console.WriteLine("文件夹不存在！");
		return files;
	}
	DirectoryInfo direInfo = new DirectoryInfo(direPath);
	var stack = new Stack<DirectoryInfo>();
	stack.Push(direInfo);

	while (stack.Count > 0)
	{
		var curDireInfo = stack.Pop();

		// 当前一级文件夹内的子文件们
		FileInfo[] curFileInfos = curDireInfo.GetFiles();
		files.AddRange(curFileInfos.Where(f => f.Extension == ".cs").ToList());

		// 当前一级文件夹内的子文件夹们
		DirectoryInfo[] subDireInfos = curDireInfo.GetDirectories();
		// 将子文件夹入栈
		foreach (DirectoryInfo d in subDireInfos)
		{
			stack.Push(d);
		}
	}

	return files;
}


class ClassInfo
{
	public string ClassName { get; set; }

	public List<PropertyInfo> Props { get; set; } = new();

	public string Comment { get; set; }
}

class PropertyInfo
{
	public string PropertyName { get; set; }

	public string Comment { get; set; }
}

