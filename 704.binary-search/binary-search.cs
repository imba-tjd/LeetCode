
namespace LeetCode.Problems.P704BinarySearch
{
    public interface ISolution { int Search(int[] nums, int target); }
    class Solution : ISolution
    {
        public int Search(int[] nums, int target) // 左开右闭区间
        {
            int from = 0, to = nums.Length;
            int midi, mid;

            while (to > from) // to - from >= 1
            {
                // midi = (from + to) / 2;
                midi = from + (to - from) / 2;
                mid = nums[midi];
                if (mid == target)
                    return midi;
                else if (mid > target)
                    to = midi;
                else
                    from = midi + 1;
            }
            return -1;
        }
    }

    class Solution2 : ISolution
    {
        public int Search(int[] nums, int target)
        {
            // if (nums.Length == 0)
            //     return -1;

            int from = 0, to = nums.Length - 1; // 闭区间
            int midi, mid;

            while (to >= from)
            {
                // midi = (from + to) / 2;
                midi = from + (to - from) / 2;
                mid = nums[midi];
                if (mid == target)
                    return midi;
                else if (mid > target)
                    to = midi - 1;
                else
                    from = midi + 1;
            }
            // return nums[from] == target ? from : -1;
            return -1;
        }
    }

    class Solution3 : ISolution
    {
        public int Search(int[] nums, int target)
        {
            if (nums.Length == 0)
                return -1;
            return BSInternal(nums, 0, nums.Length, target);
        }

        // 递归解法，而且测试速度比上面两个都快一点
        int BSInternal(int[] nums, int from, int to, int target)
        {
            if (to - from == 1)
                return nums[from] == target ? from : -1;

            int mid = from + (to - from) / 2;

            if (nums[mid] == target)
                return mid;
            else if (nums[mid] < target)
                return BSInternal(nums, mid, to, target);
            else
                return BSInternal(nums, from, mid, target);
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution2();

        [Theory]
        [InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 9, 4), InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 2, -1),
        InlineData(new int[0], 1, -1), InlineData(new[] { 1 }, 1, 0)]
        public void Test(int[] input, int target, int expect)
        {
            var result = So.Search(input, target);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
    public class Test3 : MultiTest { protected override ISolution So => new Solution3(); }
}
