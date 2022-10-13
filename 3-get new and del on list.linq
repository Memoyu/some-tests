<Query Kind="Program" />

void Main()
{
	// 过滤出需要新增、需要删除的元素
	// 入参
	var inputs = new List<string> {"1","2","3","4","5","6","7","8","9"};
	// 当前现有数据
	var currents = new List<string> { "3", "4", "9", "0" };
	for (int i = inputs.Count - 1; i >= 0; i--)
	{
		var input = inputs[i];
		var exist = currents.FirstOrDefault(s => s == input);
		if (exist != null)
		{
			currents.Remove(exist);
			inputs.Remove(input);
		}
	}
	
	Console.WriteLine("需要新增：");
	Console.WriteLine(inputs);
	Console.WriteLine("需要删除：");
	Console.WriteLine(currents);
}

