using Xunit;

namespace LeetCode.Problems.P001TwoSum;

class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var dic = new Dictionary<int, int>(nums.Length);
        for (int i = 0; i < nums.Length; i++)
            if (!dic.ContainsKey(target - nums[i]))
                dic.TryAdd(nums[i], i);
            else
                return new[] { dic[target - nums[i]], i };
        return null;
    }
}

public class UnitTest
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, 0, 1)]
    [InlineData(new[] { 1, 1, 2, 3 }, 5, 2, 3)]
    void Test(int[] nums, int target, int expect1, int expect2)
    {
        var solution = new Solution();
        int[] result = solution.TwoSum(nums, target);
        Assert.Equal(expect1, result[0]);
        Assert.Equal(expect2, result[1]);
    }
}
