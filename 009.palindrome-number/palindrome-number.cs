
namespace LeetCode.Problems.P009PalindromeNumber
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
            return x == z;
        }
    }

    class Solution3 : ISolution
    {
        public bool IsPalindrome(int x)
        {
            var arr = x.ToString().ToCharArray();
            Array.Reverse(arr);
            return x.ToString() == new string(arr);
        }
    }

    class Solution4 : ISolution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0 || x % 10 == 0 && x != 0) return false;
            int y = 0;
            while (y < x)
            {
                y = y * 10 + x % 10;
                x /= 10;
            }
            return y == x || y / 10 == x;
            // 长度为奇数时，前者不可能为true，后者就专门处理这种情况。长度为偶数时，如果前者为flase，x和y的长度就相差2，所以后者也不可能为true
        }
    }
    abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution2();

        [Theory]
        [InlineData(121, true), InlineData(-121, false), InlineData(10, false)
        , InlineData(1, true), InlineData(1001, true)]
        public void Test(int input, bool expect)
        {
            bool result = So.IsPalindrome(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
    public class Test3 : MultiTest { protected override ISolution So => new Solution3(); }
    public class Test4 : MultiTest { protected override ISolution So => new Solution4(); }
}
