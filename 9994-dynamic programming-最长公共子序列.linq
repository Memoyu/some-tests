<Query Kind="Program" />

void Main()
{
	// LeetCode:https://leetcode-cn.com/problems/longest-common-subsequence/
	Console.WriteLine(LongestCommonSubsequenceStr("abc", "def"));
	Console.WriteLine(LongestCommonSubsequence(new int[] { 1, 3, 5, 9, 10 }, new int[] { 1, 4, 9, 10 }));
	Console.WriteLine(LongestCommonSubsequence1(new int[] { 1, 3, 5, 9, 10 }, new int[] { 1, 4, 9, 10 }));
}

/// <summary>
/// 字符串同理
/// </summary>
int LongestCommonSubsequenceStr(string str1, string str2)
{
	if (str1 == null || str1.Length == 0) return 0;
	if (str2 == null || str2.Length == 0) return 0;
	var dp = new int[str1.Length, str2.Length];
	for (int i = 0; i < str1.Length; i++)
	{
		for (int j = 0; j < str2.Length; j++)
		{
			// 如果相等，则dp[i,j] = dp[i–1,j–1] + 1;
			if (str1[i] == str2[j])
			{
				var t = 0;
				if (i != 0 && j != 0) t = dp[i - 1, j - 1];
				dp[i, j] = t + 1;
			}
			else // 否则取dp[i - 1, j]、dp[i, j - 1]中最大值
			{
				var ti = 0;
				var tj = 0;
				if (i != 0) ti = dp[i - 1, j];
				if (j != 0) tj = dp[i, j - 1];
				dp[i, j] = Math.Max(ti, tj);
			}
		}
	}
	Console.WriteLine(dp);

	return dp[str1.Length - 1, str2.Length - 1];
}

/// <summary>
/// 使用等同的二维数组进行递推结果存储
/// 需要在进行dp[i-1, j-1]时进行下标校验（可能为负数）；
/// </summary>
int LongestCommonSubsequence(int[] nums1, int[] nums2)
{
	if (nums1 == null || nums1.Length == 0) return 0;
	if (nums2 == null || nums2.Length == 0) return 0;
	var dp = new int[nums1.Length, nums2.Length];
	for (int i = 0; i < nums1.Length; i++)
	{
		for (int j = 0; j < nums2.Length; j++)
		{
			// 如果相等，则dp[i,j] = dp[i–1,j–1] + 1;
			if (nums1[i] == nums2[j])
			{
				var t = 0;
				if (i != 0 && j != 0) t = dp[i - 1, j - 1];
				dp[i, j] = t + 1;
			}
			else // 否则取dp[i - 1, j]、dp[i, j - 1]中最大值
			{
				var ti = 0;
				var tj = 0;
				if (i != 0) ti = dp[i - 1, j];
				if (j != 0) tj = dp[i, j - 1];
				dp[i, j] = Math.Max(ti, tj);
			}
		}
	}
	Console.WriteLine(dp);

	return dp[nums1.Length - 1, nums2.Length - 1];
}

/// <summary>
/// 使用增大的二维数组进行递推结果存储
/// 简单明了的代码逻辑
/// </summary>
int LongestCommonSubsequence1(int[] nums1, int[] nums2)
{
	if (nums1 == null || nums1.Length == 0) return 0;
	if (nums2 == null || nums2.Length == 0) return 0;
	var dp = new int[nums1.Length + 1, nums2.Length + 1];
	for (int i = 1; i <= nums1.Length; i++)
	{
		for (int j = 1; j <= nums2.Length; j++)
		{
			// 如果相等，则dp[i,j] = dp[i–1,j–1] + 1;
			if (nums1[i - 1] == nums2[j - 1])
			{
				dp[i, j] = dp[i - 1, j - 1] + 1;
			}
			else // 否则取dp[i - 1, j]、dp[i, j - 1]中最大值
			{
				dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
			}

		}
	}
	Console.WriteLine(dp);

	return dp[nums1.Length, nums2.Length];
}

