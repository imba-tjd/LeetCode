using System;
using Xunit;

namespace LongestSubstringWithoutRepeatingCharacters
{
    class Solution
    {
        int[] dic = new int[127];
        public int LengthOfLongestSubstring(string s)
        {
            int from = 0, to = 0, longest = 0;
            while (to < s.Length)
            {
                char key = s[to];
                if (dic[key] == 0)
                    dic[key] = ++to; // index + 1
                else
                {
                    int length = to - from;
                    if (longest < length)
                        longest = length;
                    while (from < dic[key])
                        dic[s[from++]] = 0;
                }
            }
            return Math.Max(longest, to - from);
        }

        // Not My Solution
        public int LengthOfLongestSubstring2(string s)
        {
            int n = s.Length, ans = 0;
            int[] index = new int[128]; // current index of character
                                        // try to extend the range [i, j]
            for (int j = 0, i = 0; j < n; j++)
            {
                //  Console.WriteLine("");
                //  Console.Write(i);
                //  Console.Write(index[s[j]]);
                i = Math.Max(index[s[j]], i);
                //Console.Write(ans);
                ans = Math.Max(ans, j - i + 1);
                index[s[j]] = j + 1;
            }
            return ans;
        }
    }
    public class UnitTest
    {
        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        [InlineData("au", 2)]
        [InlineData("dvdf", 3)]
        [InlineData("abba", 2)]
        [InlineData(" ", 1), InlineData("", 0)]
        void Test(string s, int length)
        {
            var solution = new Solution();
            int result1 = solution.LengthOfLongestSubstring(s);
            Assert.Equal(length, result1);

            int result2 = solution.LengthOfLongestSubstring2(s);
            Assert.Equal(length, result2);
        }
    }
}
