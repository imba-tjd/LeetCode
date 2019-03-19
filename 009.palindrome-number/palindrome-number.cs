using System;
using System.Collections.Generic;
using Xunit;

namespace PalindromeNumber
{
    public interface ISolution { bool IsPalindrome(int x); }
    class Solution : ISolution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            string s = x.ToString();
            for (int i = 0; i < s.Length / 2; i++)
                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }
    }

    class Solution2 : ISolution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            int y = x;
            long z = 0;
            while (y != 0)
            {
                z = z * 10 + y % 10;
                y /= 10;
            }
            return x == z ? true : false;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(121, true), InlineData(-121, false), InlineData(10, false)
        , InlineData(1, true), InlineData(1001, true)]
        public void Test(int input, bool expect)
        {
            var so = GetSo;
            bool result = so.IsPalindrome(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
