
namespace LeetCode.Problems.P067AddBinary
{
    public interface ISolution { string AddBinary(string a, string b); }
    class Solution : ISolution
    {
        public string AddBinary(string a, string b)
        {
            var sb = new System.Text.StringBuilder();
            char carry = '0';
            for (int i = 1; i <= Math.Max(a.Length, b.Length); i++)
            {
                char result;
                char ac = a.Length - i >= 0 ? a[a.Length - i] : '0';
                char bc = b.Length - i >= 0 ? b[b.Length - i] : '0';
                (result, carry) = AddOnce(ac, bc, carry);
                sb.Insert(0, result);
            }
            if (carry == '1')
                sb.Insert(0, carry);
            return sb.ToString();
        }
        (char result, char carry) AddOnce(char a, char b, char c)
        {
            int t = a + b + c - '0' - '0' - '0';
            return ((char)((t & 1) + '0'), (char)((t >> 1) + '0'));
        }
    }
    class Solution2 : ISolution
    {
        public string AddBinary(string a, string b)
        {
            long ai = Convert.ToInt64(a, 2);
            long bi = Convert.ToInt64(b, 2);
            return Convert.ToString(ai + bi, 2);
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData("11", "1", "100"), InlineData("1010", "1011", "10101")]
        public void Test(string a, string b, string expect)
        {
            var so = GetSo;
            var result = so.AddBinary(a, b);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
