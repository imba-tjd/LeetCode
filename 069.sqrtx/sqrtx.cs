using System;
using System.Collections.Generic;
using Xunit;

namespace Sqrtx
{
    public interface ISolution { int MySqrt(int x); }
    class Solution : ISolution
    {
        public int MySqrt(int x)
        {
            int i = 0;
            while (i * i < x)
                i++;
            if (i * i > x)
                i--;
            return i;
        }
    }

    class Solution2 : ISolution
    {
        public int MySqrt(int x)
        {
            int from = 0, to = 46341;
            while (to - from > 1)
            {
                int mid = from + (to - from) / 2;
                int midSquare = mid * mid;

                if (midSquare == x)
                    return mid;
                else if (midSquare < x)
                    from = mid + 1;
                else
                    to = mid;
            }
            return from * from > x ? from - 1 : from;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(4, 2), InlineData(8, 2), InlineData(9, 3),
        InlineData(1, 1), InlineData(0, 0),
        // InlineData(2147483647, 46340)
        ]
        public void Test(int input, int expect)
        {
            var so = GetSo;
            var result = so.MySqrt(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
