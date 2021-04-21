using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Problems.P171ExcelSheetColumnNumber
{
    public interface ISolution { int TitleToNumber(string s); }
    class Solution : ISolution
    {
        public int TitleToNumber(string s)
        {
            int num = 0;
            foreach (char ch in s)
                num = num * 26 + ch - 'A' + 1;
            return num;
        }
    }

    class Solution2 : ISolution
    {
        public int TitleToNumber(string s)
        {
            int num = 0;
            for (int i = 0; i < s.Length; i++)
                num += (int)Math.Pow(26, i) * (s[s.Length - 1 - i] - 'A' + 1); // 算出当前位真正的权重再乘数
            return num;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData("A", 1), InlineData("AB", 28), InlineData("ZY", 701)]
        public void Test(string input, int expect)
        {
            var so = GetSo;
            var result = so.TitleToNumber(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
