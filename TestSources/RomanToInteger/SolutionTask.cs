using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RomanToInteger
{
	public class SolutionTask
	{
		public int RomanToInt(string str)
		{
			var dict = new Dictionary<char, int>()
			{
				{'I', 1}, {'V', 5 }, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
			};

			var result = 0;
			var current = 0;
			foreach (var letter in str)
			{
				if (current < dict[letter] && current != 0)
				{
					result += dict[letter] - current;
					current = 0;
				}
				else
				{
					result += current;
					current = dict[letter];
				}
			}
			result += current;

			return result;
		}
	}
}
