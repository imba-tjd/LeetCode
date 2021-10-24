using Xunit;

namespace LeetCode.Problems.P026RemoveDuplicatesfromSortedArray
{
    public interface ISolution { int RemoveDuplicates(int[] nums); }
    class Solution : ISolution
    {
        public int RemoveDuplicates(int[] nums)
        {
            int i = 0;
            int? pre = null;
            for (int j = 0; j < nums.Length; j++)
                if (pre != nums[j])
                    pre = nums[i++] = nums[j];
            return i;
        }
    }
}
