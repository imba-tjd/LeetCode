using System;
using System.Collections.Generic;
using Xunit;

namespace Problems.Problem028.ImplementstrStr
{
    public interface ISolution { int StrStr(string haystack, string needle); }
    class Solution : ISolution
    {
        public int StrStr(string haystack, string needle)
        {
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                // bool ok = true;
                int j = -1;
                while (++j < needle.Length)
                    if (haystack[i + j] != needle[j])
                        break; // ok = false;
                if (j == needle.Length)  // if (ok)
                    return i;
            }
            return -1;
        }
    }

    class Solution2 : ISolution
    {
        public int StrStr(string haystack, string needle)
        {
            if (needle.Length == 0)
                return 0;
            int[] next = GetNext(needle);
            int i = 0, j = 0;

            while (i < haystack.Length && j < needle.Length)
                if (j == -1 || haystack[i] == needle[j])
                {
                    i++;
                    j++;
                }
                else
                    j = next[j];

            return j == needle.Length ? i - j : -1;
        }

        public int[] GetNext(string p)
        {
            int[] next = new int[p.Length];
            next[0] = -1;
            int j = 0, k = -1;

            while (j < p.Length - 1)
                if (k == -1 || p[j] == p[k])
                    next[++j] = ++k;
                else
                    k = next[k];
            return next;
        }
    }

    // 两个方法都是错的
    class Solution3 : ISolution
    {
        public int StrStr(string haystack, string needle)
        {
            if (needle.Length == 0)
                return 0;
            int[] next = GetNext(needle);

            int j = 0;
            for (int i = 0; i < haystack.Length; i++)
            {
                if (j == needle.Length)
                    return i - j;
                else if (haystack[i] == needle[j])
                    j++;
                else if (j != 0)
                {
                    j = next[j];
                    i--;
                }
            }
            return j == needle.Length ? 0 : -1;
        }

        public int[] GetNext(string pattern)
        {
            int[] next = new int[pattern.Length];
            next[0] = -1;

            for (int j = 0; j < pattern.Length - 1; j++)
                next[j + 1] = next[j] == -1 || pattern[next[j]] == pattern[j] ? next[j] + 1 : 0;

            return next;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData("hello", "ll", 2), InlineData("aaaaa", "bba", -1), InlineData("asdf", "asdf", 0),
        InlineData("", "a", -1), InlineData("a", "", 0), InlineData("", "", 0),
        InlineData("mississippi", "issip", 4), InlineData("mississippi", "pi", 9), InlineData("aabaaabaaac", "aabaaac", 4)]
        public void Test(string input, string needle, int expect)
        {
            var so = GetSo;
            var result = so.StrStr(input, needle);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }

    public class GetNextTest
    {
        [Theory]
        [InlineData("abababcab", new[] { -1, 0, 0, 1, 2, 3, 4, 0, 1 }), InlineData("a", new[] { -1 }),
        InlineData("issip", new[] { -1, 0, 0, 0, 1 }), InlineData("aabaaac", new[] { -1, 0, 1, 0, 1, 2, 2 })]
        public void Test(string p, int[] expect)
        {
            int[] result = new Solution2().GetNext(p);

            Assert.Equal(p.Length, result.Length);
            Assert.Equal(expect, result);
        }
    }
}
