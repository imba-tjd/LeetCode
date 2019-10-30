using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.SingleNumberII
{
    public interface ISolution { int SingleNumber(int[] nums); }
    class Solution : ISolution
    {
        public int SingleNumber(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>(nums.Length);
            foreach (int n in nums)
                if (dic.ContainsKey(n))
                    dic[n]++;
                else
                    dic.Add(n, 1);
            foreach (var item in dic)
                if (item.Value == 1)
                    return item.Key;
            return -1;
        }
    }

    class Solution2 : ISolution
    {
        public int SingleNumber(int[] nums)
        {
            int a = 0, b = 0;
            foreach (int n in nums)
            {
                a ^= n & ~b;
                b ^= n & ~a;
            }
            return a;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { 2, 2, 3, 2 }, 3), InlineData(new[] { 0, 1, 0, 1, 0, 1, 99 }, 99),
        InlineData(new[] { -2, -2, 1, 1, -3, 1, -3, -3, -4, -2 }, -4)]
        public void Test(int[] input, int expect)
        {
            var so = GetSo;
            int result = so.SingleNumber(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
