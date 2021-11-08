using System.Text;

namespace LeetCode.Problems.P006ZigZagConversion
{
    public interface ISolution { string Convert(string s, int numRows); }
    class Solution : ISolution
    {
        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
                return s;
            StringBuilder[] sbl = new StringBuilder[numRows];
            for (int j = 0; j < sbl.Length; j++)
                sbl[j] = new StringBuilder();
            numRows -= 1;
            int row = 0;
            bool inc = true;
            for (int i = 0; i < s.Length; i++)
            {
                sbl[row].Append(s[i]);
                if (row == 0)
                    inc = true;
                else if (row == numRows)
                    inc = false;
                if (inc)
                    row++;
                else
                    row--;
            }
            return string.Concat(sbl as object[]);
        }
    }
    class Solution2 : ISolution
    {
        public string Convert(string s, int numRows)
        {
            if (numRows == 1) // 不可省略
                return s;
            StringBuilder sb = new StringBuilder(s.Length);
            int cycleLen = 2 * numRows - 2;
            for (int row = 0; row < numRows; row++)
                for (int j = 0; j + row < s.Length; j += cycleLen) // j+row即第一个数字的位置
                {
                    sb.Append(s[j + row]);
                    int second = j + cycleLen - row;
                    if (row != 0 && row != numRows - 1 && second < s.Length)
                        sb.Append(s[second]);
                }

            return sb.ToString();
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR"), InlineData("PAYPALISHIRING", 4, "PINALSIGYAHRPI"),
        InlineData("AB", 1, "AB"), InlineData("", 0, "")]
        public void Test(string input, int numRows, string expect)
        {
            var so = GetSo;
            var result = so.Convert(input, numRows);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
