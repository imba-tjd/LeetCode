using System;
using System.Collections.Generic;
using Xunit;

namespace CountandSay
{
    public interface ISolution { string CountAndSay(int n); }
    class Solution : ISolution
    {
        public string CountAndSay(int n)
        {
            string s = "1";
            while (--n > 0)
                s = Gen(s);
            return s;
        }
        public string Gen(string s)
        {
            var sb = new System.Text.StringBuilder();
            int i = 0;
            while (i < s.Length)
            {
                char ch = s[i];
                int count = 0;
                while (i < s.Length && s[i] == ch)
                {
                    count++;
                    i++;
                }
                sb.Append(count.ToString() + ch);
            }
            return sb.ToString();
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData(1, "1"), InlineData(4, "1211")]
        public void Test(int input, string expect)
        {
            var so = GetSo;
            var result = so.CountAndSay(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }

    public class GenTest
    {
        [Theory]
        [InlineData("1", "11"), InlineData("11", "21"), InlineData("21", "1211"), InlineData("1211", "111221")]
        void Test(string input, string expect)
        {
            var so = new Solution();
            string result = so.Gen(input);
            Assert.Equal(expect, result);
        }
    }
}
