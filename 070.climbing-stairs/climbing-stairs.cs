using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.ClimbingStairs
{
    public interface ISolution { int ClimbStairs(int n); }
    class Solution : ISolution
    {
        public int ClimbStairs(int n)
        {
            int[] stairs = new int[n + 1];
            stairs[0] = 1;
            for (int i = 0; i < n + 1; i++)
            {
                if (i < n)
                    stairs[i + 1] += stairs[i];
                if (i + 1 < n)
                    stairs[i + 2] += stairs[i];
            }
            return stairs[n];
        }
    }

    class Solution2 : ISolution
    {
        public int ClimbStairs(int n)
        {
            int a, b, c;
            a = 1;
            b = c = 0;
            for (int i = 0; i < n; i++)
            {
                if (i < n)
                    b += a;
                if (i + 1 < n)
                    c += a;
                (a, b, c) = (b, c, 0);
            }
            return a;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(2, 2), InlineData(3, 3), InlineData(4, 5)]
        public void Test(int input, int expect)
        {
            var so = GetSo;
            var result = so.ClimbStairs(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
