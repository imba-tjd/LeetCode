
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

    class Solution2 : ISolution
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
                return 0;

            int i = 0;
            for (int j = 0; j < nums.Length; j++)
                if (nums[i] != nums[j])
                    nums[++i] = nums[j];
            return i + 1;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution();

        [Theory]
        [InlineData(new[] { 1, 1, 2 }, new[] { 1, 2 })]
        [InlineData(new int[]{}, new int[]{})]
        public void Test(int[] input, int[] expect)
        {
            int n = So.RemoveDuplicates(input);
            int[] result = input[0..(n)];
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
