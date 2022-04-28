namespace LeetCode.Problems.P020ValidParentheses;

public interface ISolution { bool IsValid(string s); }
class Solution : ISolution
{
    public bool IsValid(string s)
    {
        if (s.Length == 0)
            return true;
        // 可省略的优化，但可能导致隐藏下面的BUG，故不开启
        // if (s.Length % 2 == 1)
        //     return false;

        Stack<char> stack = new Stack<char>(s.Length / 2);
        Dictionary<char, char> map = new Dictionary<char, char>(3)
            {
                {'(',')'},
                {'[',']'},
                {'{','}'}
            };

        foreach (char ch in s)
            if ("([{".Contains(ch))
                stack.Push(ch);
            else if (!stack.TryPop(out char ch2) || ch != map[ch2]) // 也可用stack.count==0 || ch!=map[stack.Pop()]
                return false;
        return stack.Count == 0;
    }
}

class Solution2 : ISolution
{
    public bool IsValid(string s)
    {
        int l;
        do
        {
            l = s.Length;
            s = s.Replace("()", "").Replace("[]", "").Replace("{}", "");
        } while (l != s.Length);
        return s.Length == 0;
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution();

    [Theory]
    [InlineData("()", true), InlineData("()[]{}", true), InlineData("{[]}", true),
    InlineData("(]", false), InlineData("([)]", false),
    InlineData("", true), InlineData(")", false), InlineData("(", false)]
    public void Test(string input, bool expect)
    {
        bool result = So.IsValid(input);
        Assert.Equal(expect, result);
    }
}
public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
