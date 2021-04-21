using System;
using Xunit;

namespace LeetCode.Problems.P268MissingNumber
{
    public interface ISolution { int MissingNumber(int[] nums); }
    class Solution : ISolution
    {
        public int MissingNumber(int[] nums)
        {
            Array.Sort(nums);
            int from = 0, to = nums.Length, mid;
            while (from < to)
            {
                mid = from + (to - from) / 2;
                if (nums[mid] == mid)
                    from = mid + 1;
                else
                    to = mid;
            }
            return to;
        }
    }

    class Solution2 : ISolution
    {
        public int MissingNumber(int[] nums)
        {
            int[] hs = new int[nums.Length + 1];
            foreach (int num in nums)
                hs[num] = 1;
            for (int i = 0; i < nums.Length; i++)
                if (!(hs[i] == 1))
                    return i;
            return nums.Length;
        }
    }

    class Solution3 : ISolution
    {
        public int MissingNumber(int[] nums)
        {
            int result = nums.Length;
            for (int i = 0; i < nums.Length; i++)
                result ^= i ^ nums[i];
            return result;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData(new[] { 1 }, 0), InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { 3, 0, 1 }, 2)]
        [InlineData(new[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8)]
        public void Test(int[] input, int expect)
        {
            var so = GetSo;
            int result = so.MissingNumber(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { protected override ISolution GetSo => new Solution3(); }
}
