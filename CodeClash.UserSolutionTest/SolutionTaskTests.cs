using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CodeClash.UserSolutionTest
{
  [TestFixture]
  public class SolutionTaskTests
  {
    private SolutionTask solution = new();
    private void RunTest(int[] nums, int target, int[] expected)
    {
      var result = solution.FindSum(nums, target);
      CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void FindSumTest_Example1()
    {
      var nums = new[] { 1, 1 };
      var target = 2;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void FindSumTest_Example2()
    {
      var nums = new[] { 2, 7, 11, 3 };
      var target = 9;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void FindSumTest_Example3()
    {
      var nums = new[] { 1, 2, 3, 4 };
      var target = 6;
      var expected = new[] { 1, 3 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void OneElementIsZero()
    {
      var nums = new[] { 5, 0, 3, 4 };
      var target = 5;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void OneElementIsNegative()
    {
      var nums = new[] { 11, 9, -5, 3, 20 };
      var target = 15;
      var expected = new[] { 2, 4 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void AllElementsIsNegative()
    {
      var nums = new[] { -3, -1, -5, -9, -2 };
      var target = -10;
      var expected = new[] { 1, 3 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void LargeArray()
    {
      var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
      var target = 19;
      var expected = new[] { 8, 9 };
      RunTest(nums, target, expected);
    }


    [Test]
    public void DuplicatesInArray()
    {
      var nums = new[] { 1, 2, 3, 3, 4 };
      var target = 6;
      var expected = new[] { 2, 3 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void NegativeAndPositiveMix()
    {
      var nums = new[] { -2, 1, 4, -1, 5 };
      var target = 2;
      var expected = new[] { 0, 2 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void LargeNumbers()
    {
      var nums = new[] { 1000000, 500000, -1500000, 2000000 };
      var target = 1500000;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void ArrayContainsZero()
    {
      var nums = new[] { 0, 2, 8, 5 };
      var target = 8;
      var expected = new[] { 0, 2 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void ZeroTarget()
    {
      var nums = new[] { 1, -1, 2, -5 };
      var target = 0;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void RepeatedTarget()
    {
      var nums = new[] { 1, 2, 2, 5, 9 };
      var target = 4;
      var expected = new[] { 1, 2 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void VeryLargeArray()
    {
      var nums = new int[100000];
      for (int i = 0; i < nums.Length; i++) nums[i] = i + 1;
      var target = 199999;
      var expected = new[] { 99998, 99999 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void SameNumberTwice()
    {
      var nums = new[] { 3, 3, 4, 5 };
      var target = 6;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }


    [Test]
    public void MixedZerosAndNegatives()
    {
      var nums = new[] { 0, -1, -2, -3, -4 };
      var target = -3;
      var expected = new[] { 1, 2 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void LargeNegativeTarget()
    {
      var nums = new[] { -100, -200, -300, -400 };
      var target = -500;
      var expected = new[] { 1, 2 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void AlternatingSigns()
    {
      var nums = new[] { 1, -1, 2, -4, 3, -5 };
      var target = 0;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void PositiveAndNegativeSumToZero()
    {
      var nums = new[] { 3, -3, 4, 5 };
      var target = 0;
      var expected = new[] { 0, 1 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void ArrayWithMinMaxSumToTarget()
    {
      var nums = new[] { 1, 2, 10, 20, 50 };
      var target = 52;
      var expected = new[] { 1, 4 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void AllNegativesSumToNegativeTarget()
    {
      var nums = new[] { -5, -10, -15, -20 };
      var target = -30;
      var expected = new[] { 1, 3 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void NumbersSeparatedByZeros()
    {
      var nums = new[] { 3, 0, 0, 7 };
      var target = 10;
      var expected = new[] { 0, 3 };
      RunTest(nums, target, expected);
    }

    [Test]
    public void NegativeAndPositiveSumToZero()
    {
      var nums = new[] { -9, 3, -3, 5 };
      var target = 0;
      var expected = new[] { 1, 2 };
      RunTest(nums, target, expected);
    }


    [Test]
    public void NumbersLeadingToTargetValue()
    {
      var nums = new[] { 2, 4, 6, 8, 10, 12, 13 };
      var target = 20;
      var expected = new[] { 3, 5 };
      RunTest(nums, target, expected);
    }
  }
}