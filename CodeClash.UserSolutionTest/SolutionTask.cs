namespace CodeClash.UserSolutionTest;
public class SolutionTask
{
	public int[] FindSum(int[] nums, int target)
	{
		int[] array = new int[2];
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if ((nums[i] + nums[j]) == target)
                {
                    array[0] = i;
                    array[1] = j;
                }
            }
        }
        return array;
	}
}
