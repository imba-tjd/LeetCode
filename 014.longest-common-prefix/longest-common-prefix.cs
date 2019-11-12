using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.Problem014.LongestCommonPrefix
{
    public interface ISolution { string LongestCommonPrefix(string[] strs); }
    class Solution : ISolution
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
                return "";
            else if (strs.Length == 1)
                return strs[0];

            for (int i = 0; i < strs[0].Length; i++)
            {
                char ch = strs[0][i];
                foreach (var str in strs)
                    if (i == str.Length || str[i] != ch)
                        return strs[0].Substring(0, i);
            }
            return strs[0];
        }
    }

    class Solution2 : ISolution
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
                return "";

            var span = strs.AsSpan();
            var arr1 = span.Slice(0, span.Length / 2).ToArray();
            var arr2 = span.Slice(span.Length / 2, span.Length / 2 + 1).ToArray();
            var task1 = System.Threading.Tasks.Task.Run(() => LongestCommonPrefixInternal(arr1));
            var task2 = System.Threading.Tasks.Task.Run(() => LongestCommonPrefixInternal(arr2));
            System.Threading.Tasks.Task.WhenAll(task1, task2);
            return LongestCommonPrefixInternal(new[] { task1.Result, task2.Result });
        }
        string LongestCommonPrefixInternal(string[] strs)
        {
            var method = typeof(Solution).GetMethod(nameof(LongestCommonPrefix));
            return method.Invoke(new Solution(), new object[] { strs }) as string;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData(new[] { "flower", "flow", "flight" }, "fl"),
        InlineData(new[] { "dog", "racecar", "car" }, ""),
        InlineData(new[] { "" }, ""), InlineData(new string[0], "")]
        public void Test(string[] input, string expect)
        {
            var so = GetSo;
            var result = so.LongestCommonPrefix(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
