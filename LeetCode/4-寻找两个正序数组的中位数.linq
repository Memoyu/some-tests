<Query Kind="Program" />

void Main()
{
	// 4. 寻找两个正序数组的中位数 https://leetcode.cn/problems/median-of-two-sorted-arrays/
	// - 重点在于合并两数组

	var nums1 = new int[] { 1, 3 };
	var nums2 = new int[] { 2 };
	var mid = FindMedianSortedArrays(nums1, nums2);
}

public double FindMedianSortedArrays(int[] nums1, int[] nums2)
{
	var mid = 0d;
	var newNums = new int[nums1.Length + nums2.Length];
	if (nums1.Length < nums2.Length)
	{
		var temp = nums1 ;
		nums1 = nums2;
		nums2 = temp;
	}
	
	var q = new Queue<int>();
	for (int i = 0; i < nums2.Length; i++)
	{
		q.Enqueue(nums2[i]);
	}
	
	
	for (int i = 0; i < nums1.Length; i++)
	{
		var num = q.Peek();
		if (nums1[1] > n)
		{
			
		}
	}
	return mid;
}