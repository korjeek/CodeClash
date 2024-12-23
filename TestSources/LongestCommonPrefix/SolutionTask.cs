using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.LongestCommonPrefix
{
	public class SolutionTask
	{
		public string LongestCommonPrefix(string[] strs)
		{
			if (strs.Length == 0) return "";
			var firstWord = strs[0];
			var prefix = firstWord;

			foreach (var word in strs)
			{
				var pairs = prefix.Zip(word, (char1, char2) => new { Letter1 = char1, Letter2 = char2 });
				var prefix2 = new StringBuilder();
				foreach (var pair in pairs)
				{
					if (pair.Letter1 == pair.Letter2)
						prefix2.Append(pair.Letter1);
					else
						break;
				}
				prefix = prefix2.ToString();
				if (prefix.Length == 0)
					return string.Empty;
			}

			return prefix;
		}
	}
}