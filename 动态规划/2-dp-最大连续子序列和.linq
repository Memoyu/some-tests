<Query Kind="Program" />

void Main()
{
	// LeetCode:https://leetcode.cn/problems/maximum-subarray/
	
	Console.WriteLine(MaxSubArray(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
	Console.WriteLine(MaxSubArray1(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
}

/// <summary>
/// 只关注最终结果，只保留以i - 1结尾的最大子序列和，然后计算i的最大子序列和
/// 空间复杂度为O1
/// </summary>
int MaxSubArray1(int[] nums)
{
	if (nums == null || nums.Length == 0) return 0;
	var max = nums[0];
	var prevMax = max;
	for (int i = 1; i < nums.Length; i++)
	{
		if (prevMax <= 0)
		{
			prevMax = nums[i];
		}
		else
		{
			prevMax = prevMax + nums[i];
		}
		max = Math.Max(max, prevMax);
	}

	return max;
}

/// <summary>
/// 使用数组存储以nums[i]结尾的最大连续子序列的和
/// </summary>
int MaxSubArray(int[] nums)
{
	if (nums == null || nums.Length == 0) return 0;
	var maxs = new int[nums.Length];
	var max = maxs[0] = nums[0];
	for (int i = 1; i < nums.Length; i++)
	{
		var prev = maxs[i - 1];
		if (prev <= 0)
		{
			maxs[i] = nums[i];
		}
		else
		{
			maxs[i] = prev + nums[i];
		}

		max = Math.Max(max, maxs[i]);
	}

	return max;
}

