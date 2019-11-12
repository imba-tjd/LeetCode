using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.Problem027.RemoveElement
{
    public interface ISolution { int RemoveElement(int[] nums, int val); }
    class Solution : ISolution
    {
        public int RemoveElement(int[] nums, int val)
        {
            if (nums.Length == 0)
                return 0;
            int p = 0, q = nums.Length - 1;
            while (p < q)
            {
                while (p < q && nums[p] != val)
                    p++;
                if (p == q)
                    goto outside;

                while (p < q && nums[q] == val)
                    q--;
                if (p == q)
                    goto outside;

                nums[p] = nums[q];
                nums[q] = val;
                p++;
                q--;
            }
        outside:
            return nums[p] == val ? p : p + 1;
        }
    }

    class Solution2 : ISolution
    {
        public int RemoveElement(int[] nums, int val)
        {
            int p = 0;
            for (int i = 0; i < nums.Length; i++)
                if (nums[i] != val)
                    nums[p++] = nums[i];
            return p;
        }
    }

    class Solution3 : ISolution
    {
        public int RemoveElement(int[] nums, int val)
        {
            if (nums.Length == 0)
                return 0;
            else if (nums.Length == 1)
                return nums[0] == val ? 0 : 1;
            int p = 0, q = nums.Length - 1;
            while (p < q)
                if (nums[p] != val)
                    p++;
                else
                {
                    if (nums[q] != val)
                        nums[p] = nums[q];
                    q--;
                }
            return nums[p] == val ? p : p + 1;
        }
    }

    class Solution4 : ISolution
    {
        public int RemoveElement(int[] nums, int val)
        {
            int p = 0, q = nums.Length;
            while (p < q)
                if (nums[p] != val)
                    p++;
                else
                    nums[p] = nums[--q];
            return q;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution4();

        [Theory]
        [InlineData(new[] { 3, 2, 2, 3 }, 3, 2), InlineData(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5),
        InlineData(new[] { 1 }, 1, 0), InlineData(new[] { 1 }, 0, 1), InlineData(new int[0], 1, 0),
        InlineData(new[] { 3, 3 }, 3, 0), InlineData(new[] { 1, 1, 0 }, 1, 1), InlineData(new[] { 1, 0 }, 1, 1)]
        public void Test(int[] input, int val, int expect)
        {
            var so = GetSo;
            var result = so.RemoveElement(input, val);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { override protected ISolution GetSo => new Solution3(); }
    public class Test4 : MultiTest { override protected ISolution GetSo => new Solution4(); }
}
