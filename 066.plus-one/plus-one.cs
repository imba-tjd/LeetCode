using Xunit;

namespace LeetCode.Problems.P066PlusOne
{
    public interface ISolution { int[] PlusOne(int[] digits); }
    class Solution : ISolution
    {
        public int[] PlusOne(int[] digits)
        {
            int carry = 1;
            Span<int> span = stackalloc int[digits.Length + 1];
            for (int i = digits.Length - 1; i >= 0; i--)
                (span[i + 1], carry) = AddOnce(digits[i], carry);
            span[0] = carry;

            return span[0] == 0 ? span.Slice(1).ToArray() : span.ToArray();
        }

        (int value, int carry) AddOnce(int a, int b) =>
            ((a + b) % 10, a + b >= 10 ? 1 : 0);
    }

    class Solution2 : ISolution
    {
        public int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
                if (++digits[i] != 10)
                    return digits;
                else
                    digits[i] = 0;
            var arr = new int[digits.Length + 1]; // 无法使用初始化器，因为数组的只能用常量长度且要与初始化的数量匹配
            arr[0] = 1;
            // Array.Copy(digits, 0, arr, 1, digits.Length);
            return arr;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 4 }), InlineData(new[] { 9 }, new[] { 1, 0 }), InlineData(new[] { 0 }, new[] { 1 })]
        public void Test(int[] input, int[] expect)
        {
            var so = GetSo;
            var result = so.PlusOne(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
