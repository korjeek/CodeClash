using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Valid_Parentheses
{
	public class SolutionTask
	{
		public bool IsValid(string s)
		{
			var dict = new Dictionary<char, char>() { { '(', ')' }, { '[', ']'}, { '{', '}'} };
			var stack = new Stack<char>();
			foreach (var bracket in s)
			{
				if (dict.ContainsKey(bracket))
					stack.Push(bracket);
				else if (stack.Count == 0 || dict[stack.Pop()] != bracket)
					return false;
			}
			return stack.Count() == 0;
		}
	}
}
