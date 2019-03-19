using System;
using Xunit;

namespace LongestPalindromicSubstring
{
    public interface ISolution { string LongestPalindrome(string s); }
    class Solution : ISolution
    {
        public string LongestPalindrome(string s) // BF
        {
            if (s.Length == 1)
                return s;
            var result = string.Empty.AsSpan();
            var span = s.AsSpan();
            for (int i = 0; i < s.Length - 1; i++)
                for (int j = i + 1; j <= s.Length; j++)
                {
                    var ss = span.Slice(i, j - i);
                    if (IsPalindromicString(ss) == true && ss.Length > result.Length)
                        result = ss;
                }
            return result.ToString();
        }
        public bool IsPalindromicString(ReadOnlySpan<char> s)
        {
            if (s.Length == 1)
                return true;
            for (int i = 0; i < s.Length / 2; i++)
                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }
    }

    public abstract class MultiTest
    {
        protected abstract ISolution GetSo { get; }

        [Theory]
        [InlineData("babad", "bab")]
        [InlineData("cbbd", "bb")]
        [InlineData("a", "a")]
        [InlineData("bb", "bb")]
        public void Test(string input, string expect)
        {
            var so = GetSo;
            string result = so.LongestPalindrome(input);
            Assert.Equal(expect, result);
        }
    }
    public class BFTest : MultiTest
    {
        protected override ISolution GetSo => new Solution();

        [Theory]
        [InlineData("bab", true), InlineData("bb", true)]
        [InlineData("baba", false)]
        [InlineData("a", true)]
        void IsPalindromicStringTest(string s, bool expect)
        {
            var so = new Solution();
            bool result = so.IsPalindromicString(s);
            Assert.Equal(expect, result);
        }

    }
}
