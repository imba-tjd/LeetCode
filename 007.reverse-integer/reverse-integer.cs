using System;
using Xunit;
// using System.Linq;

namespace ReverseInteger
{
    public interface ISolution { int Reverse(int x); }
    public class Solution : ISolution
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
    }
    public class Solution2 : ISolution // 字符串反转
    {
        public int Reverse(int x)
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

    public abstract class MultiTest
    {
        protected abstract ISolution GetSo { get; }

        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(1534236469, 0)]
        [InlineData(-2147483648, 0)]
        public void Test(int input, int expect)
        {
            var so = GetSo;
            int result = so.Reverse(input);
            Assert.Equal(expect, result);
        }
    }

    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
