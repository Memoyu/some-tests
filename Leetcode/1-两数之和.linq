<Query Kind="Program" />

void Main()
{
	//1. 两数之和：https://leetcode.cn/problems/two-sum/
	var nums = new int[] { 3, 2, 4 };
	var target = 6;
	var res = TwoSum(nums, target);
}

public int[] TwoSum(int[] nums, int target)
{
	// 使用字典存储nums, num为key，num的index为value
	var dic = new Dictionary<int, int>();
	// 遍历nums, 同时将num存入字典
	for (int i = 0; i < nums.Length; i++)
	{
		// 比对是否存在两个num之和等于target
		var num = target - nums[i];
		if (dic.ContainsKey(num))
		{
			return new int[] { dic[num], i };
		}
		// 插入字典(使用TryAdd，因为可能存在相同的num数值, 则会存在相同的key，导致add异常)
		dic.TryAdd(nums[i], i);
	}

	return new int[2];
}