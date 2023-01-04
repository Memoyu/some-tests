<Query Kind="Program" />

void Main()
{
	// 2. 两数相加：https://leetcode.cn/problems/add-two-numbers/
	var l1 = BuildLinkedList(new List<int> { 2, 4, 3 });
	var l2 = BuildLinkedList(new List<int> { 5, 6, 4 });
	var sumNode = AddTwoNumbers(l1, l2);
}

public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
{
	var root = new ListNode(0);
	var cur = root;
	int carry = 0;
	while (l1 != null || l2 != null)
	{
		var val1 = l1?.val ?? 0;
		var val2 = l2?.val ?? 0;
		var sum = val1 + val2 + carry;
		carry = sum / 10;
		sum = sum % 10;

		cur.next = new ListNode(sum);
		cur = cur.next;

		l1 = l1?.next;
		l2 = l2?.next;
	}

	// 如果最后一次求和仍然是 >= 10 的，则需要进一位，则最后一个元素值则为carry
	if (carry == 1)
	{
		cur.next = new ListNode(carry);
	}

	return root.next;
}

public ListNode BuildLinkedList(List<int> list)
{
	// list.Reverse();
	var root = new ListNode(list[0]);
	ListNode prev = root;
	for (int i = 1; i < list.Count; i++)
	{
		var node = new ListNode(list[i]);
		prev.next = node;
		prev = node;
	}

	return root;
}

public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int val = 0, ListNode next = null)
	{
		this.val = val;
		this.next = next;
	}
}

// You can define other methods, fields, classes and namespaces here