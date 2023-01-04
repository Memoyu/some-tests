<Query Kind="Program" />

void Main()
{
	// 3. 无重复字符的最长子串：https://leetcode.cn/problems/longest-substring-without-repeating-characters/
	var max = LengthOfLongestSubstring("acbba");
	Console.WriteLine(max);
}

public int LengthOfLongestSubstring(string s)
{
	// 用于存储每个字符对应字符串中的下标（重复时则会更新为最后出现的字符的下标）
	Dictionary<char, int> chars = new Dictionary<char, int>();
	// 窗口起始下标
	int left = 0;
	// 最长无重复最长字串数
	int max = 0;
	// 遍历字符串
	for (int i = 0; i < s.Length; i++)
	{
		// 如果chars中存在当前字符，则需要对left进行更新；
		// 此时，分为两种情况：
		// - 存在字符时，并且字符包含在有效的子段中
		// 		如：abca，在遍历到第二个a时，之前符合条件的字段为abc，而当前则需要更新left = chars['a'] + 1 = 1，且符合条件的字段为bca；
		// - 存在字符时，并且字符不包含在有效的子段中
		// 		如：abba，在遍历a、b之后，left仍然为0，但是到第二个b时，则出现了如上情况，此时left = chars['b'] + 1 = 2，子段为b；
		//			再继续碰到a，如果仍然按照上述进行处理 left = chars['a'] + 1 = 0 + 1 = 1，显然上次的left为2，现在却为1，
		// 			left倒退了肯定时错误的，所以与需要使用Math.Max取出最大的left，保持在此种情况下left不变；
		if (chars.ContainsKey(s[i]))
		{
			left = Math.Max(left, chars[s[i]] + 1);
			chars[s[i]] = i;// 更新字符下标
		}
		else
		{
			chars.Add(s[i], i);// 更新字符下标
		}

		// 获取符合需求的最大子段长度
		max = Math.Max(max, i - left + 1);
	}

	return max;
}
