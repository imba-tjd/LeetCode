using System;
using System.Collections.Generic;
using Xunit;

namespace BinarySearch
{
    public interface ISolution { int Search(int[] nums, int target); }
    class Solution : ISolution
    {
        public int Search(int[] nums, int target)
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

            int from = 0, to = nums.Length - 1;
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

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 9, 4), InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 2, -1),
        InlineData(new int[0], 1, -1), InlineData(new[] { 1 }, 1, 0)]
        public void Test(int[] input, int target, int expect)
        {
            var so = GetSo;
            var result = so.Search(input, target);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
