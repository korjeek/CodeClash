using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClash.UserSolutionTest
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
				MakeListNode(new int[] { 3, 4 }),
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
				MakeListNode(new int[] { 1 }),
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

		[Test]
		public void MergeTwoLists_BothListsNull()
		{
			RunTest(null, null, null);
		}

		[Test]
		public void MergeTwoLists_OneListNull()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 3 }),
				null,
				MakeListNode(new int[] { 1, 2, 3 })
			);
		}

		[Test]
		public void MergeTwoLists_OneListSingleNode()
		{
			RunTest(
				MakeListNode(new int[] { 1 }),
				MakeListNode(new int[] { 2 }),
				MakeListNode(new int[] { 1, 2 })
			);
		}

		[Test]
		public void MergeTwoLists_SameValuesRepeating()
		{
			RunTest(
				MakeListNode(new int[] { 2, 2, 2 }),
				MakeListNode(new int[] { 2, 2 }),
				MakeListNode(new int[] { 2, 2, 2, 2, 2 })
			);
		}

		[Test]
		public void MergeTwoLists_LargeNumbers()
		{
			RunTest(
				MakeListNode(new int[] { int.MinValue, int.MaxValue }),
				MakeListNode(new int[] { 0 }),
				MakeListNode(new int[] { int.MinValue, 0, int.MaxValue })
			);
		}

		[Test]
		public void MergeTwoLists_DescendingInput()
		{
			RunTest(
				MakeListNode(new int[] { 5, 6, 7 }),
				MakeListNode(new int[] { 1, 2, 3 }),
				MakeListNode(new int[] { 1, 2, 3, 5, 6, 7 })
			);
		}

		[Test]
		public void MergeTwoLists_SingleElementOverlap()
		{
			RunTest(
				MakeListNode(new int[] { 1 }),
				MakeListNode(new int[] { 1 }),
				MakeListNode(new int[] { 1, 1 })
			);
		}

		[Test]
		public void MergeTwoLists_FirstListSubsetOfSecond()
		{
			RunTest(
				MakeListNode(new int[] { 2, 4 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5 }),
				MakeListNode(new int[] { 1, 2, 2, 3, 4, 4, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_SecondListSubsetOfFirst()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 3, 4, 5 }),
				MakeListNode(new int[] { 2, 4 }),
				MakeListNode(new int[] { 1, 2, 2, 3, 4, 4, 5 })
			);
		}

		[Test]
		public void MergeTwoLists_AlternateElements()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[] { 2, 4, 6 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_AllNegativeValues()
		{
			RunTest(
				MakeListNode(new int[] { -5, -3, -1 }),
				MakeListNode(new int[] { -4, -2, 0 }),
				MakeListNode(new int[] { -5, -4, -3, -2, -1, 0 })
			);
		}

		[Test]
		public void MergeTwoLists_AllZeros()
		{
			RunTest(
				MakeListNode(new int[] { 0, 0, 0 }),
				MakeListNode(new int[] { 0, 0 }),
				MakeListNode(new int[] { 0, 0, 0, 0, 0 })
			);
		}

		[Test]
		public void MergeTwoLists_FirstListLonger()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5, 7, 9 }),
				MakeListNode(new int[] { 2, 4 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 7, 9 })
			);
		}

		[Test]
		public void MergeTwoLists_SecondListLonger()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2 }),
				MakeListNode(new int[] { 3, 4, 5, 6, 7 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 6, 7 })
			);
		}

		[Test]
		public void MergeTwoLists_LargeLists()
		{
			RunTest(
				MakeListNode(Enumerable.Range(1, 1000).ToArray()),
				MakeListNode(Enumerable.Range(1001, 1000).ToArray()),
				MakeListNode(Enumerable.Range(1, 2000).ToArray())
			);
		}

		[Test]
		public void MergeTwoLists_LargeValuesOnly()
		{
			RunTest(
				MakeListNode(new int[] { int.MaxValue - 1, int.MaxValue }),
				MakeListNode(new int[] { int.MinValue, int.MinValue + 1 }),
				MakeListNode(new int[] { int.MinValue, int.MinValue + 1, int.MaxValue - 1, int.MaxValue })
			);
		}

		[Test]
		public void MergeTwoLists_InterleavingLists()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 5 }),
				MakeListNode(new int[] { 2, 4, 6 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_SingleRepeatedValue()
		{
			RunTest(
				MakeListNode(new int[] { 2, 2, 2 }),
				MakeListNode(new int[] { 2, 2 }),
				MakeListNode(new int[] { 2, 2, 2, 2, 2 })
			);
		}

		[Test]
		public void MergeTwoLists_AllElementsSame()
		{
			RunTest(
				MakeListNode(new int[] { 3, 3, 3 }),
				MakeListNode(new int[] { 3, 3, 3 }),
				MakeListNode(new int[] { 3, 3, 3, 3, 3, 3 })
			);
		}

		[Test]
		public void MergeTwoLists_DifferentRanges()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 3 }),
				MakeListNode(new int[] { 4, 5, 6 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 5, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_EmptyAndNonEmpty()
		{
			RunTest(
				MakeListNode(new int[0]),
				MakeListNode(new int[] { 1, 2, 3 }),
				MakeListNode(new int[] { 1, 2, 3 })
			);
		}

		[Test]
		public void MergeTwoLists_AllElementsNegative()
		{
			RunTest(
				MakeListNode(new int[] { -10, -5, -1 }),
				MakeListNode(new int[] { -8, -4, -2 }),
				MakeListNode(new int[] { -10, -8, -5, -4, -2, -1 })
			);
		}

		[Test]
		public void MergeTwoLists_AllElementsPositive()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3, 7 }),
				MakeListNode(new int[] { 2, 4, 6 }),
				MakeListNode(new int[] { 1, 2, 3, 4, 6, 7 })
			);
		}

		[Test]
		public void MergeTwoLists_VeryLargeLists()
		{
			RunTest(
				MakeListNode(Enumerable.Range(1, 500).ToArray()),
				MakeListNode(Enumerable.Range(501, 500).ToArray()),
				MakeListNode(Enumerable.Range(1, 1000).ToArray())
			);
		}

		[Test]
		public void MergeTwoLists_FirstListNullSecondNonEmpty()
		{
			RunTest(
				null,
				MakeListNode(new int[] { 1, 2, 3 }),
				MakeListNode(new int[] { 1, 2, 3 })
			);
		}

		[Test]
		public void MergeTwoLists_SecondListNullFirstNonEmpty()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 3 }),
				null,
				MakeListNode(new int[] { 1, 2, 3 })
			);
		}

		[Test]
		public void MergeTwoLists_AlternateNegativeAndPositive()
		{
			RunTest(
				MakeListNode(new int[] { -3, -1 }),
				MakeListNode(new int[] { 0, 2, 4 }),
				MakeListNode(new int[] { -3, -1, 0, 2, 4 })
			);
		}

		[Test]
		public void MergeTwoLists_OverlappingRanges()
		{
			RunTest(
				MakeListNode(new int[] { 1, 2, 5 }),
				MakeListNode(new int[] { 2, 3, 6 }),
				MakeListNode(new int[] { 1, 2, 2, 3, 5, 6 })
			);
		}

		[Test]
		public void MergeTwoLists_OneEmptyList()
		{
			RunTest(
				MakeListNode(new int[] { }),
				MakeListNode(new int[] { }),
				null
			);
		}

		[Test]
		public void MergeTwoLists_AlternatingSmallLists()
		{
			RunTest(
				MakeListNode(new int[] { 1, 3 }),
				MakeListNode(new int[] { 2, 4 }),
				MakeListNode(new int[] { 1, 2, 3, 4 })
			);
		}
	}
}


