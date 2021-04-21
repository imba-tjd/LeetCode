using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Problems.P125ValidPalindrome
{
    public interface ISolution { bool IsPalindrome(string s); }
    class Solution : ISolution
    {
        public bool IsPalindrome(string s)
        {
            if (s.Length == 0)
                return true;
            s = s.ToLowerInvariant();
            int from = 0, to = s.Length - 1;
            while (from < to)
            {
                if (!char.IsLetterOrDigit(s[from]))
                    from++;
                else if (!char.IsLetterOrDigit(s[to]))
                    to--;
                else if (s[from] != s[to])
                    return false;
                else
                {
                    from++;
                    to--;
                }
            }
            return true;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData("", true), InlineData("0P", false),
        // InlineData("A man, a plan, a canal: Panama", true), InlineData("race a car", false)
        ]
        public void Test(string input, bool expect)
        {
            var so = GetSo;
            var result = so.IsPalindrome(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
