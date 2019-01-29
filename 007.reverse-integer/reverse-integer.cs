using System;
using Xunit;
// using System.Linq;

namespace ReverseInteger
{
    class Solution
    {
        public int Reverse(int x)
        {
            int result = 0;
            try
            {
                while (x != 0)
                {
                    result = checked(result * 10 + x % 10);
                    x /= 10;
                }
            }
            catch (OverflowException)
            {
                result = 0;
            }
            return result;
        }
        public int Reverse2(int x)
        {
            if (x == int.MinValue)
                return 0;
            var arr = Math.Abs(x).ToString().ToCharArray();
            Array.Reverse(arr);
            int.TryParse(arr, out int result);
            if (x < 0)
                result = -result;
            return result;
        }
    }

    public class UnitTest
    {
        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(1534236469, 0)]
        [InlineData(-2147483648, 0)]
        void Test(int input, int expect)
        {
            var solution = new Solution();
            int result = solution.Reverse(input);
            Assert.Equal(expect, result);

            int result2 = solution.Reverse2(input);
            Assert.Equal(expect, result2);
        }
    }
}
