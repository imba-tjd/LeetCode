using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.SingleNumber
{
    public interface ISolution { int SingleNumber(int[] nums); }
    class Solution : ISolution
    {
        public int SingleNumber(int[] nums)
        {
            int result = 0;
            foreach (int n in nums)
                result ^= n;
            return result;
        }
    }

    class Solution2 : ISolution
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
    class Solution3 : ISolution
    {
        public int SingleNumber(int[] nums)
        {
            HashSet<int> hs = new HashSet<int>(nums.Length);
            foreach (int n in nums)
                if (hs.Contains(n))
                    hs.Remove(n);
                else
                    hs.Add(n);
            foreach (var item in hs)
                return item;
            return -1;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData(new[] { 2, 2, 1 }, 1), InlineData(new[] { 4, 1, 2, 1, 2 }, 4), InlineData(new[] { 1 }, 1)]
        public void Test(int[] input, int expect)
        {
            var so = GetSo;
            int result = so.SingleNumber(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { override protected ISolution GetSo => new Solution3(); }
}
