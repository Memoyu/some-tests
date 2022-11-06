<Query Kind="Program" />

void Main()
{
	//Console.WriteLine(LCSubString("ABCD", "BABC"));// 使用一维数组，加上curr标识基础上进行优化，减少dp数组长度
	//Console.WriteLine(LCSubString_2("ABCD", "BABC"));// 使用一维数组，加上curr标识进行实现；
	//Console.WriteLine(LCSubString_1("ABCD", "BABC"));// 基础实现，使用二维数组进行结果集记录

	Console.WriteLine(LCSubString("ABCBA", "BABCA"));
	Console.WriteLine(LCSubString_2("ABCBA", "BABCA"));
	Console.WriteLine(LCSubString_1("ABCBA", "BABCA"));
}

/// <summary>
/// 使用一维数组，加上curr标识基础上进行优化，减少dp数组长度
/// 进一步减少占用内存
/// </summary>
int LCSubString(string str1, string str2)
{
	if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2)) return 0;

	// 进行最短string参数获取，并赋值给colStr
	string rowStr = str1, colStr = str2;
	if (str1.Length < str2.Length)
	{
		rowStr = str2;
		colStr = str1;
	}

	var dp = new int[colStr.Length + 1];
	var max = 0;

	// 外层为rowStr
	for (int i = 1; i <= rowStr.Length; i++)
	{
		var curr = 0;
		for (int j = 1; j <= colStr.Length; j++)
		{
			var leftTop = curr;
			curr = dp[j];

			// 不相等时需要进行制空，因为数组的重用的，不清除则会残留上次的结果
			if (rowStr[i - 1] != colStr[j - 1])
			{
				dp[j] = 0;
				continue;
			}
			dp[j] = leftTop + 1;
			max = Math.Max(max, dp[j]);
		}
	}
	Console.WriteLine(dp);
	return max;
}

/// <summary>
/// 使用一维数组，加上curr标识进行实现
/// 减少占用内存
/// </summary>
int LCSubString_2(string str1, string str2)
{
	if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2)) return 0;
	var dp = new int[str2.Length + 1];
	var max = 0;
	for (int i = 1; i <= str1.Length; i++)
	{
		var curr = 0;
		for (int j = 1; j <= str2.Length; j++)
		{
			var leftTop = curr;
			curr = dp[j];
			// 不相等时需要进行制空，因为数组的重用的，不清除则会残留上次的结果
			if (str1[i - 1] != str2[j - 1])
			{
				dp[j] = 0;
				continue;
			}
			dp[j] = leftTop + 1;
			max = Math.Max(max, dp[j]);
		}
		//Console.WriteLine($"i={i}:");
		//Console.WriteLine(dp);
	}
	Console.WriteLine(dp);
	return max;
}

/// <summary>
/// 使用二维数组存储结果集
/// </summary>
int LCSubString_1(string str1, string str2)
{
	if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2)) return 0;
	var dp = new int[str1.Length + 1, str2.Length + 1];
	var max = 0;
	for (int i = 1; i <= str1.Length; i++)
	{
		for (int j = 1; j <= str2.Length; j++)
		{
			if (str1[i - 1] != str2[j - 1]) continue;
			dp[i, j] = dp[i - 1, j - 1] + 1;
			max = Math.Max(max, dp[i, j]);
		}
	}
	Console.WriteLine(dp);
	return max;
}