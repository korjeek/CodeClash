using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.MergeTwoSortedLists
{
	[TestFixture]
	public class SolutionTaskTests
	{
		private SolutionTask solution = new SolutionTask();

		private void RunTest(ListNode list1, ListNode list2, ListNode expected)
		{
			var result = solution.MergeTwoLists(list1, list2);
			Assert.That(AreEqual(result, expected), Is.True, "The linked lists are not equal.");
		}

		private ListNode MakeListNode(int[] arr)
		{
			if (arr.Length == 0)
				return null;

			var result = new ListNode(arr[0]);
			var current = result;

			for (var i = 1; i < arr.Length; i++)
			{
				current.next = new ListNode(arr[i]);
				current = current.next;
			}

			return result;
		}

		private bool AreEqual(ListNode node1, ListNode node2)
		{
			while (node1 != null && node2 != null)
			{
				if (node1.val != node2.val)
					return false;

				node1 = node1.next;
				node2 = node2.next;
			}

			return node1 == null && node2 == null;
		}

		[Test]
		public void MergeTwoLists_Example1()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 5 }),
				MakeListNode(new int[] {3, 4}),
				MakeListNode(new int[] { 1, 2, 3, 4, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_Example2()
		{
			RunTest(
				MakeListNode(new int[0]),
				MakeListNode(new int[0]),
				MakeListNode(new int[0])
			);
		}

		[Test]
		public void MergeTwoLists_Example3()
		{
			RunTest(
				MakeListNode(new int[] {1}),
				MakeListNode(new int[0]),
				MakeListNode(new int[] { 1 })
			);
		}
		[Test]
		public void MergeTwoLists_SameValueInLists()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[] { 1, 1, 3, 3, 5, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_DifferentLengths()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3 }),
				MakeListNode(new int[] { 2, 4, 5 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_FirstListEmpty()
		{
			RunTest(
				MakeListNode(new int[0]),
				MakeListNode(new int[] { 2, 4, 6 }),
				MakeListNode(new int[] { 2, 4, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_SecondListEmpty()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[0]),
				MakeListNode(new int[] { 1, 3, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_NoCommonValues()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[] { 2, 4, 6 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_MixedValues()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 5 }),
				MakeListNode(new int[] { 1, 3, 4 }),
				MakeListNode(new int[] { 1, 1, 2, 3, 4, 5 })
			);
		}
	}
}

