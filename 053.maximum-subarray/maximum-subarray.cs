namespace LeetCode.Problems.P053MaximumSubarray;

public interface ISolution { int MaxSubArray(int[] nums); }
class Solution : ISolution
{
    public int MaxSubArray(int[] nums)
    {
        int maxsum = int.MinValue, sum = int.MinValue;
        // int len = 0, maxlen = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            if (sum > maxsum)
            {
                maxsum = sum;
                // maxlen = ++len;
            }

            if (sum < 0)
            {
                sum = 0;
                // len = 0;
            }
        }
        return maxsum;
    }
}

// abstract
public class MultiTest
{
    protected virtual ISolution GetSo => new Solution();

    [Theory]
    [InlineData(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6),
    InlineData(new int[0], 0), InlineData(new[] { 1 }, 1), InlineData(new[] { -1 }, -1)]
    public void Test(int[] input, int expect)
    {
        var so = GetSo;
        var result = so.MaxSubArray(input);
        Assert.Equal(expect, result);
    }
}
// public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
// public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
