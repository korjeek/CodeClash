namespace CodeClash.UserSolutionTest;
public class SolutionTask
{
  public int[] FindSum(int[] nums, int target)
  {
    Dictionary<int, int> hashtable = new Dictionary<int, int>();
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            int complement = target - nums[i];
            if (hashtable.TryGetValue(complement, out var value))
                return new[] { value, i };
            hashtable[nums[i]] = i;
        }

        return new int[] { };

  }
}
