<Query Kind="Program" />

void Main()
{
	Coins(41);
	CoinsUniversal(41, new int[] { 1, 5, 20, 25 });
}

/// <summary>
/// 通用实现
/// </summary>
int CoinsUniversal(int n, int[] faces)
{
	if (n < 1 || faces == null || faces.Length < 1) return -1;
	int[] dp = new int[n + 1];
	int[] elements = new int[dp.Length];

	for (int i = 1; i <= n; i++)
	{
		int min = int.MaxValue;
		foreach (var face in faces)
		{
			if (i < face) continue;
			var v = dp[i - face];
			if (v < 0 || v >= min) continue;
			min = v;
			elements[i] = face;
		}

		dp[i] = min + 1;
	}

	Console.WriteLine($"需要 {dp[n]} 枚硬币，具体如下");
	print(elements, n);
	return dp[n];
}

/// <summary>
/// 实现具体剖析
/// </summary>
int Coins(int n)
{
	if (n < 1) return -1;
	int[] dp = new int[n + 1];
	int[] elements = new int[dp.Length];

	for (int i = 1; i <= n; i++)
	{
		int min = int.MaxValue;
		if (i >= 1 && dp[i - 1] < min)
		{
			min = dp[i - 1];
			elements[i] = 1;
		}
		if (i >= 5 && dp[i - 5] < min)
		{
			min = dp[i - 5];
			elements[i] = 5;
		}
		if (i >= 20 && dp[i - 20] < min)
		{
			min = dp[i - 20];
			elements[i] = 20;
		}
		if (i >= 25 && dp[i - 25] < min)
		{
			min = dp[i - 25];
			elements[i] = 25;
		}

		dp[i] = min + 1;
	}

	//for (int j = 1; j <= n; j++)
	//{
	//	Console.WriteLine(j + " = " + dp[j]);
	//}
	Console.WriteLine($"需要 {dp[n]} 枚硬币，具体如下");
	print(elements, n);
	return dp[n];
}

void print(int[] faces, int n)
{
	while (n > 0)
	{
		Console.WriteLine("C：" + faces[n]);
		n -= faces[n];
	}
}


