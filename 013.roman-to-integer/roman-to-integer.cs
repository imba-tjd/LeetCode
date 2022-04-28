
namespace LeetCode.Problems.P013RomantoInteger
{
    public interface ISolution { int RomanToInt(string s); }
    class Solution : ISolution
    {
        public int RomanToInt(string s)
        {
            int pre, total, now;
            pre = total = 0;
            for (int i = 0; i < s.Length; i++)
            {
                now = GetInt(s[i]);
                if (now > pre)
                    total -= 2 * pre;
                total += now;
                pre = now;
            }
            return total;
        }
        [System.Runtime.CompilerServices.MethodImpl
        (System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        int GetInt(char c)
        {
            switch (c)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
                default: throw new NotSupportedException($"'{c}' is not a Roman caracter!");
            }
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution();

        [Theory]
        [InlineData("III", 3), InlineData("IV", 4), InlineData("IX", 9),
        InlineData("LVIII", 58), InlineData("MCMXCIV", 1994)]
        public void Test(string input, int expect)
        {
            var result = So.RomanToInt(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
