<Query Kind="Program" />

void Main()
{
	// 计算日期间隔天数
	var ts = DateTime.Parse("2022-10-11 14:30:00").Subtract(DateTime.Parse("2022-10-10 14:40:30"));
	Console.WriteLine(ts.Days + "天");
	Console.WriteLine(ts.Hours + "小时");
	Console.WriteLine(ts.Minutes + "分钟");
	Console.WriteLine(ts.Seconds + "秒");

	var b = DateTime.Parse("2022-10-10 14:30:00");
	var e = DateTime.Parse("2022-10-15 14:30:00");
	var freq = 1;
	var t = b;
	var list = new List<DateTime>();
	while ((t = t.AddDays(freq)) <= e)
	{
		list.Add(t);
		Console.WriteLine(t);
	}

	Console.WriteLine();
}

