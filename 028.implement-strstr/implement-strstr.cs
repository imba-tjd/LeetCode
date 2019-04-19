using System;
using System.Collections.Generic;
using Xunit;

namespace ImplementstrStr
{
    public interface ISolution { int StrStr(string haystack, string needle); }
    class Solution : ISolution
    {
        public int StrStr(string haystack, string needle)
        {
            for (int i = 0; i + needle.Length <= haystack.Length; i++)
            {
                int j = -1;
                while (++j < needle.Length)
                    if (haystack[i + j] != needle[j])
                        break;
                if (j == needle.Length)
                    return i;

                // bool ok = true;
                // for (int j = 0; j < needle.Length; j++)
                //     if (haystack[i + j] != needle[j])
                //     {
                //         ok = false;
                //         break;
                //     }
                // if (ok)
                //     return i;
            }
            return -1;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData("hello", "ll", 2), InlineData("aaaaa", "bba", -1), InlineData("asdf", "asdf", 0),
        InlineData("", "a", -1), InlineData("a", "", 0), InlineData("", "", 0)
        ]
        public void Test(string input, string needle, int expect)
        {
            var so = GetSo;
            var result = so.StrStr(input, needle);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
