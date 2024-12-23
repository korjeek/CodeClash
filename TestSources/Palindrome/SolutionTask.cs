using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Palindrome
{
	public class SolutionTask
	{
		public bool IsPalindrome(int number)
		{
			var str = number.ToString();
			for (var i=0; i<str.Length; i++)
			{
				if (str[i] != str[^(i+1)])
					return false;
			};
			return true;
		}
	}
}
