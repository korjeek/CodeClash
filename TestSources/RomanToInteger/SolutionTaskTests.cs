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
		private void RunTest(string str, int expected)
		{
			var result = solution.RomanToInt(str);
			Assert.That(expected, Is.EqualTo(result));
		}

		[Test]
		public void RomanToInt_Example1() => RunTest("III", 3);

		[Test]
		public void RomanToInt_Example2() => RunTest("LVIII", 58);

		[Test]
		public void RomanToInt_Example3() => RunTest("MCMXCIV", 1994);

		[Test]
		public void RomanToInt_LongStr() => RunTest("MMMMMMMMMMMMMMMMMMMMMMMMM", 25000);

		[Test]
		public void RomanToInt_numIs4() => RunTest("IV", 4);

		[Test]
		public void RomanToInt_numIs9() => RunTest("IX", 9);

		[Test]
		public void RomanToInt_numIs19() => RunTest("XIX", 19);

		[Test]
		public void RomanToInt_numIs90() => RunTest("XC", 90);

		[Test]
		public void RomanToInt_SingleCharacterI() => RunTest("I", 1);

		[Test]
		public void RomanToInt_SingleCharacterM() => RunTest("M", 1000);

		[Test]
		public void RomanToInt_TwoCharacters() => RunTest("VI", 6);

		[Test]
		public void RomanToInt_SubtractionPattern4() => RunTest("IV", 4);

		[Test]
		public void RomanToInt_SubtractionPattern9() => RunTest("IX", 9);

		[Test]
		public void RomanToInt_ComplexCase1987() => RunTest("MCMLXXXVII", 1987);

		[Test]
		public void RomanToInt_EmptyString() => RunTest("", 0);


		[Test]
		public void RomanToInt_RepeatedCharacters() => RunTest("IIII", 4);

		[Test]
		public void RomanToInt_ComplexLargeNumber() => RunTest("MMMDCCCLXXXVIII", 3888);

		[Test]
		public void RomanToInt_MultipleSubtractions() => RunTest("XCIX", 99);

		[Test]
		public void RomanToInt_LongStringOfThousands() => RunTest("MMMM", 4000);

		[Test]
		public void RomanToInt_InvalidOrder() => RunTest("MII", 1002);

		[Test]
		public void RomanToInt_MultipleAdditions() => RunTest("CLX", 160);

		[Test]
		public void RomanToInt_AllUniqueCharacters() => RunTest("IVXLCDM", 1444);

		[Test]
		public void RomanToInt_RandomOrder() => RunTest("DCLXVI", 666);

		[Test]
		public void RomanToInt_AllThousands() => RunTest("MMM", 3000);

		[Test]
		public void RomanToInt_EdgeCase3999() => RunTest("MMMCMXCIX", 3999);

		[Test]
		public void RomanToInt_InvalidCharacterInString() => RunTest("MCMXCIIVX", 2005);

		[Test]
		public void RomanToInt_LargeNumber1()
		{
			RunTest("MMMMMMMMMMMMMMMMMMMMMMMMM", 25000);
		}

		[Test]
		public void RomanToInt_LargeNumber2()
		{
			RunTest("MMMDCCCLXXXVIII", 3888);
		}

		[Test]
		public void RomanToInt_LargeNumber3()
		{
			RunTest("MMMM", 4000);
		}

		[Test]
		public void RomanToInt_LargeNumber4()
		{
			RunTest("MMMCMXCIX", 3999);
		}

		[Test]
		public void RomanToInt_LargeNumber5()
		{
			RunTest("MMMMM", 5000);
		}

		[Test]
		public void RomanToInt_LargeNumber6()
		{
			RunTest("MMMMMMMMMMMMMMMMMMMMMMMMMMM", 27000);
		}

		[Test]
		public void RomanToInt_LargeNumber7()
		{
			RunTest("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM", 63000);
		}
	}
}
