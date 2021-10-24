using Xunit;

namespace LeetCode.Problems.P005LongestPalindromicSubstring
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
        internal bool IsPalindromicString(ReadOnlySpan<char> s)
        {
            if (s.Length == 1)
                return true;
            for (int i = 0; i < s.Length / 2; i++)
                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }
    }

    class Solution2 : ISolution
    {
        public string LongestPalindrome(string s) // DP
        {
            ReadOnlySpan<char> ss = s.AsSpan();
            ReadOnlySpan<char> str = "";
            for (int i = 0; i < s.Length; i++)
            {
                var r = Expand(ss, i - 1, i + 1);
                if (r.Length > str.Length)
                    str = r;
                r = Expand(ss, i, i + 1);
                if (r.Length > str.Length)
                    str = r;
            }
            return str.ToString();
        }
        internal ReadOnlySpan<char> Expand(ReadOnlySpan<char> s, int from, int to) // 后两者表示将要评估的位置
        {
            while (from >= 0 && to <= s.Length - 1)
            {
                if (s[from] != s[to])
                    break;
                from--;
                to++;
            }

            return from + 1 == to ? "" : s.Slice(from + 1, to - from - 1);
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData("babad", "bab"), InlineData("cbbd", "bb"), InlineData("cbbb", "bbb")]
        [InlineData("a", "a"), InlineData("bb", "bb")]
        public void Test(string input, string expect)
        {
            var so = GetSo;
            string result = so.LongestPalindrome(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }

    public class BFTest
    {
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

    public class DPTest
    {
        [Theory]
        [InlineData("a", -1, 1), InlineData("bab", 0, 2),
        InlineData("bb", 0, 1), InlineData("abba", 1, 2)]
        void ExpandTest1(string s, int from, int to)
        {
            var so = new Solution2();
            string r = so.Expand(s, from, to).ToString();
            Assert.Equal(s, r);
        }
        [Fact]
        void ExpandTest2()
        {
            var so = new Solution2();
            string r = so.Expand("cb", 0, 1).ToString();
            Assert.Equal("", r);
        }
        [Fact]
        void ExpandTest3()
        {
            var so = new Solution2();
            string r = so.Expand("cbbb", 1, 2).ToString();
            Assert.Equal("bb", r);
        }
    }
}
