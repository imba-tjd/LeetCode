
namespace LeetCode.Problems.P020ValidParentheses
{
    public interface ISolution { bool IsValid(string s); }
    class Solution : ISolution
    {
        public bool IsValid(string s)
        {
            if (s.Length == 0)
                return true;
            // if (s.Length % 2 == 1)
            //     return false;

            Stack<char> stack = new Stack<char>(s.Length / 2);
            int index = -1;
            while (++index < s.Length)
                switch (s[index])
                {
                    case '(':
                    case '[':
                    case '{':
                        stack.Push(s[index]);
                        break;
                    case ')':
                    case ']':
                    case '}':
                        if (stack.Count == 0 || stack.Pop() != Match(s[index]))
                            return false;
                        break;
                }
            return stack.Count == 0;
        }
        char Match(char c)
        {
            switch (c)
            {
                case ')': return '(';
                case ']': return '[';
                case '}': return '{';
            }
            throw new InvalidOperationException();
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData("()", true), InlineData("()[]{}", true), InlineData("{[]}", true),
        InlineData("(]", false), InlineData("([)]", false),
        InlineData("", true), InlineData(")", false), InlineData("(", false)]
        public void Test(string input, bool expect)
        {
            var so = GetSo;
            bool result = so.IsValid(input);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
