<Query Kind="Program" />

void Main()
{
	// LeetCode:https://leetcode-cn.com/problems/longest-increasing-subsequence/
	Console.WriteLine(LengthOfLIS(new int[] { 1,3,6,7,9,4,10,5,6 }));
}

int LengthOfLIS(int[] nums)
{
	if (nums == null || nums.Length == 0) return 0;
	var maxs = new int[nums.Length];
	var max = maxs[0] = 1;
	for (int i = 1; i < nums.Length; i++)
	{
		// 默认以nums[i]结尾的最长升子序列为1
		maxs[i] = 1;
		// 获取j = [0,i)中以nums[i]结尾的最长升子序列，加1作为i的的最长升子序列
		for (int j = i - 1; j >= 0; j--)
		{
			// nums[i] <= nums[j] 则证明无法满足【严格递增】，则抛弃
			if (nums[i] <= nums[j]) continue;
			maxs[i] = Math.Max(maxs[i], maxs[j] + 1);
		}
		max = Math.Max(maxs[i], max);
	}
	return max;
}