using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.DeleteDuplicates
{
	public class SolutionTask
	{
		public (int, int[]) DeleteDuplicates(int[] nums)
		{
			var result = 0;
			var results = new List<int>();
			var set = new HashSet<int>();
			foreach ( var n in nums)
			{
				if (set.Contains(n))
					result++;
				else
				{
					set.Add(n);
					results.Add(n);
				}
			}
			results.Sort();
			return (result, results.ToArray());
		} 
	}
}
