 # ������� ��� ��������������� �����!

��� ������ ����������� � ������� ListNode, �� ��� ����������� � �������!
```
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
```

�� ���� �� ��������� ��� ������� ***ListNode***, ��� ��� ������������� ����� �� �����������.
���������� ��� ���� ����� � ������� ����, � ������� ��� �������� ���� ���� �� ����������� (����� ����� �����������)! 


* ������ �1
> **Input**: list1 = 1 -> 2 -> 5, list2 = 3 -> 4
> **Output**: 1 -> 2 -> 3 -> 4 -> 5

* ������ �2
> **Input**: list1 - ������, list2 - ������
> **Output**: ������ ListNode


* ������ �3
> **Input**: list1 = 1, list2 - ������
> **Output**: 1


