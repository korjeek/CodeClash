using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RomanToInteger
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
	}
}
