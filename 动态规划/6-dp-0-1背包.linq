<Query Kind="Program" />

void Main()
{
	int[] values = { 6, 3, 5, 4, 6 };
	int[] weights = { 2, 2, 6, 5, 4 };
	var capacity = 10;

	// 装满背包条件：恰好等于背包承重capacity
	Console.WriteLine(MaxValue_ExactlyEqual(values, weights, capacity));

	// 装满背包条件：不超过背包承重capacity
	Console.WriteLine(MaxValue(values, weights, capacity));
	Console.WriteLine(MaxValue_2(values, weights, capacity));
	Console.WriteLine(MaxValue_1(values, weights, capacity));
}

#region 装满背包条件：恰好等于背包承重capacity

/// <summary>
/// 获取刚好凑满capacity重量的最大价值，凑不齐则返回-1
/// </summary>
int MaxValue_ExactlyEqual(int[] values, int[] weights, int capacity)
{
	if (values == null || values.Length == 0) return 0;
	if (weights == null || weights.Length == 0) return 0;
	if (values.Length != weights.Length) return 0;
	if (capacity <= 0) return 0;
	var dp = new int[capacity + 1];
	
	// 初始化dp数组为 负无穷大 int.MinValue
	// 之所以是负无穷大，是为了为了规避dp[j - weights[i - 1]] + values[i - 1]为正数的情况导致结果错误
	for (int i = 1; i <= capacity; i++)
	{
		dp[i] = int.MinValue;
	}

	for (int i = 1; i <= values.Length; i++)
	{
		for (int j = capacity; j >= weights[i - 1]; j--)
		{
			dp[j] = Math.Max(
				dp[j],
				dp[j - weights[i - 1]] + values[i - 1]
			);
		}

	}
	Console.WriteLine(dp);
	return dp[capacity] < 0 ? -1 : dp[capacity];// 对结果进行判断
}

#endregion


#region 装满背包条件：不超过背包承重capacity

/// <summary>
/// 进一步优化MaxValue_2
/// 减少了循环次数
/// </summary>
int MaxValue(int[] values, int[] weights, int capacity)
{
	if (values == null || values.Length == 0) return 0;
	if (weights == null || weights.Length == 0) return 0;
	if (values.Length != weights.Length) return 0;
	if (capacity <= 0) return 0;
	var dp = new int[capacity + 1];
	for (int i = 1; i <= values.Length; i++)
	{
		// 观察可发现，当背包总承重量j 小于 最后一个物品的重量weights[i - 1]时,
		// 则小于weights[i - 1]的dp[j]始终都是不变的；
		// 故，循环只需要处理大于weights[i - 1]的dp[j]；
		for (int j = capacity; j >= weights[i - 1]; j--)
		{
			dp[j] = Math.Max(
				dp[j],
				dp[j - weights[i - 1]] + values[i - 1]
			);
		}

	}
	Console.WriteLine(dp);
	return dp[capacity];
}

/// <summary>
/// 使用以为数据存储需要使用的结果集
/// </summary>
int MaxValue_2(int[] values, int[] weights, int capacity)
{
	if (values == null || values.Length == 0) return 0;
	if (weights == null || weights.Length == 0) return 0;
	if (values.Length != weights.Length) return 0;
	if (capacity <= 0) return 0;
	var dp = new int[capacity + 1];
	for (int i = 1; i <= values.Length; i++)
	{
		// 从规律中可知：dp(i,j)都是由dp(i–1,k) {k≤j} 推导出来的
		// 且可以发现，j - weights[i - 1] 始终是小于j的；
		// 如果从0到capacity的顺序去计算，且使用一维数组，则会覆盖小于j的数据；
		// 反之，从capacity到0的顺序去计算，则只会覆盖大于j的数据；
		// 所以，使用降序的形式进行计算，从最大的capacity算起；
		for (int j = capacity; j > 0; j--)
		{
			// 当 weights[i - 1] > j 时，不需要操作，因为此时最大的价值就是dp[j]，则无需赋值；	
			if (weights[i - 1] < j)
			{
				dp[j] = Math.Max(
					dp[j],
					dp[j - weights[i - 1]] + values[i - 1]
				);
			}
		}

	}
	Console.WriteLine(dp);
	return dp[capacity];
}


/// <summary>
/// 使用多维数组进行结果集存储
/// </summary>
int MaxValue_1(int[] values, int[] weights, int capacity)
{
	if (values == null || values.Length == 0) return 0;
	if (weights == null || weights.Length == 0) return 0;
	if (values.Length != weights.Length) return 0;
	if (capacity <= 0) return 0;
	var dp = new int[values.Length + 1, capacity + 1];
	for (int i = 1; i <= values.Length; i++)
	{
		for (int j = 1; j <= capacity; j++)
		{
			if (weights[i - 1] > j)
			{
				dp[i, j] = dp[i - 1, j];
			}
			else
			{
				dp[i, j] = Math.Max(
					dp[i - 1, j], // 如果不选这i物品的最大价值
					dp[i - 1, j - weights[i - 1]] + values[i - 1] // 如果选了i这个物品的最大价值
				);
			}
		}

	}
	Console.WriteLine(dp);
	return dp[values.Length, capacity];
}

#endregion
