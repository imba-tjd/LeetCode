using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.Problem008.StringtoIntegeratoi
{
    public interface ISolution { int MyAtoi(string str); }
    class Solution : ISolution
    {
        public int MyAtoi(string str)
        {
            var span = str.AsSpan();
            span = span.TrimStart(' ');
            if (span.Length == 0)
                return 0;

            bool negative = false;
            int position = 0;
            if (span[position] == '-')
            {
                negative = true;
                position++;
            }
            else if (span[position] == '+')
                position++;

            int value = 0;
            while (position < span.Length && char.IsDigit(span[position]))
                try
                {
                    checked { value = value * 10 + (span[position] - '0'); }
                    position++;
                }
                catch
                {
                    return negative ? int.MinValue : int.MaxValue;
                }
            return negative ? -value : value;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData("42", 42), InlineData("   -42", -42), InlineData("4193 with words", 4193),
        InlineData("words and 987", 0), InlineData("-91283472332", -2147483648), InlineData("", 0),
        InlineData("2147483646", 2147483646)]
        public void Test(string input, int expect)
        {
            var so = GetSo;
            var result = so.MyAtoi(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
