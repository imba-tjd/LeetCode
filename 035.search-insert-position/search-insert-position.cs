using System;
using System.Collections.Generic;
using Xunit;

namespace SearchInsertPosition
{
    public interface ISolution { int SearchInsert(int[] nums, int target); }
    class Solution : ISolution
    {
        public int SearchInsert(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
                if (nums[i] >= target)
                    return i;
            return nums.Length;
        }
    }

    class Solution2 : ISolution
    {
        public int SearchInsert(int[] nums, int target)
        {
            int from = 0, to = nums.Length;
            int midi, mid;

            while (from < to)
            {
                midi = from + (to - from) / 2;
                mid = nums[midi];
                if (mid == target)
                    return midi;
                else if (mid > target)
                    to = midi;
                else
                    from = midi + 1;
            }
            return from;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { 1, 3, 5, 6 }, 5, 2), InlineData(new[] { 1, 3, 5, 6 }, 2, 1), InlineData(new[] { 1, 3, 5, 6 }, 7, 4),
        InlineData(new[] { 1, 3, 5, 6 }, 0, 0), InlineData(new int[0], 1, 0), InlineData(new[] { 0, 1 }, 2, 2)]
        public void Test(int[] input, int target, int expect)
        {
            var so = GetSo;
            var result = so.SearchInsert(input, target);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
