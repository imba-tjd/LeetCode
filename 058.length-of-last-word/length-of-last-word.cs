
namespace LeetCode.Problems.P058LengthofLastWord
{
    public interface ISolution { int LengthOfLastWord(string s); }
    class Solution : ISolution
    {
        public int LengthOfLastWord(string s)
        {
            s = s.Trim(); // 可以用AsSpan但没必要
            int len = 0;
            foreach (char ch in s)
            {
                if (ch == ' ')
                    len = 0;
                else len++;
            }
            return len;
        }
    }


    class Solution2 : ISolution
    {
        public int LengthOfLastWord(string s)
        {
            s = s.Trim();
            return s.Length - s.LastIndexOf(' ') - 1;
        }
    }

    class Solution3 : ISolution
    {
        public int LengthOfLastWord(string s)
        {
            int len1 = 0, len2 = 0;
            foreach (char ch in s)
                if (ch == ' ')
                    len2 = 0;
                else
                    len1 = ++len2;
            return len1;
        }
    }

    // TODO: 好像需要多测试
    // abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution2();

        [Theory]
        [InlineData("Hello World", 5), InlineData("asdf", 4), InlineData("  ", 0), InlineData("a ", 1)]
        public void Test(string input, int expect)
        {
            var result = So.LengthOfLastWord(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
