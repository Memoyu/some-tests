<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
	var json = "{\"name\": \"jack\",\"title\": \"lj\",\"age\": 18,\"friends\": [{\"id\":1,\"title\":\"篮球\"},{\"id\":2,\"title\":\"唱跳\"}], \"idCard\": {\"id\": 18231, \"date\":\"2022-10-09 09:19\"} }";
	var jo = JObject.Parse(json);
	Console.WriteLine(jo["name"]);
	// 替换string值
	jo["name"] = "new jack";
	Console.WriteLine(jo["friends"]);
	// 替换Array值
	// 构造数组，并转化为JArray
	var fs = new List<object> { new {id = 1, title="篮球-1"}, new {id = 2, title="篮球-2"}, new {id = 3, title="篮球-3"}};
	jo["friends"] = JArray.FromObject(fs);

	// 替换Object值
	// 构造对象实例，并转化为JObject
	var id = new {id = 21221, date = DateTime.Now};
	jo["idCard"] = JObject.FromObject(id);

	// 插入新值
	jo["like"] = JArray.FromObject(new List<object> { new { type = "sport", name = "足球"}, new { type = "food", name = "蛋糕"}});
	
	Console.WriteLine(jo.ToString());
}

