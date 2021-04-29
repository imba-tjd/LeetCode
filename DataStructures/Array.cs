using System.Text;
using Xunit;

namespace LeetCode.DataStructures.Array
{
    public static class ArrayExtension
    {
        public static string ToStringEx<T>(this T[] arr)
        {
            var sb = new StringBuilder();
            sb.Append('[');
            foreach (T item in arr)
                sb.AppendFormat("{0}, ", item);
            if (sb.Length > 1)
                sb.Remove(sb.Length - 2, 2);
            sb.Append(']');
            return sb.ToString();
        }
    }
}

namespace LeetCode.DataStructures.Array.Test
{
    public class ToStringExTest
    {
        [Theory]
        [InlineData(new int[] { }, "[]")]
        [InlineData(new[] { 1, 2, 3 }, "[1, 2, 3]")]
        public void Test(int[] arr, string expect)
        {
            Assert.Equal(expect, arr.ToStringEx());
        }
    }
}
