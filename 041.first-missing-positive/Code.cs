namespace LeetCode.Problems.P041FirstMissingPositive;

public interface ISolution { int FirstMissingPositive(int[] nums); }
class Solution : ISolution
{
    public int FirstMissingPositive(int[] nums)
    {
        bool[] records = new bool[nums.Length + 1]; // 记录1-n，不使用0。这其实是一种手动哈希表，与用HashSet区别不大
        foreach (int num in nums)
            if (num > 0 && num <= nums.Length)
                records[num] = true;
        for (int i = 1; i < nums.Length + 1; i++)
            if (records[i] == false)
                return i;
        return nums.Length + 1;
    }
}

class Solution2 : ISolution
{
    public int FirstMissingPositive(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] <= 0)
                nums[i] = nums.Length + 1;
        foreach (int num in nums)
        {
            int num2 = Math.Abs(num);
            if (num2 <= nums.Length && nums[num2 - 1] > 0)
                nums[num2 - 1] = -nums[num2 - 1];
        }
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] > 0)
                return i + 1;
        return nums.Length + 1;
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution2();

    [Theory]
    [InlineData(new[] { 1, 2, 0 }, 3), InlineData(new[] { 3, 4, -1, 1 }, 2),
    InlineData(new[] { 7, 8, 9, 11, 12 }, 1), InlineData(new[] { 1 }, 2), InlineData(new[] { 1, 1 }, 2)]
    public void Test(int[] input, int expect)
    {
        var result = So.FirstMissingPositive(input);
        Assert.Equal(expect, result);
    }
}
public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
