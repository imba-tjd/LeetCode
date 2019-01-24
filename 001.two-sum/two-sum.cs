using System;
using System.Collections.Generic;
using Xunit;

class TwoSumSolution
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

public class TwoSumTest
{
    TwoSumSolution twoSum = new TwoSumSolution();

    [Fact]
    void Test1()
    {
        int[] result = twoSum.TwoSum(new[] { 2, 7, 11, 15 }, 9);
        Assert.Equal(0, result[0]);
        Assert.Equal(1, result[1]);
    }
    [Fact]
    void Test2()
    {
        int[] result = twoSum.TwoSum(new[] { 1, 1, 2, 3 }, 5);
        Assert.Equal(2, result[0]);
        Assert.Equal(3, result[1]);
    }
}
