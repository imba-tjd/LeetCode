namespace LeetCode.Problems.P189RotateArray;

public interface ISolution { void Rotate(int[] nums, int k); }
class Solution : ISolution
{
    public void Rotate(int[] nums, int k)
    {
        k = k % nums.Length;
        int[] arr = new int[k];
        Array.Copy(nums, nums.Length - k, arr, 0, k);
        Array.Copy(nums, 0, nums, k, nums.Length - k);
        Array.Copy(arr, 0, nums, 0, k);
    }
}

class Solution2 : ISolution
{
    public void Rotate(int[] nums, int k)
    {
        k = k % nums.Length;
        Array.Reverse(nums);
        Array.Reverse(nums, 0, k);
        Array.Reverse(nums, k, nums.Length - k);
    }
}

// 错的
class Solution3 : ISolution
{
    public void Rotate(int[] nums, int k)
    {
        k = k % nums.Length;
        for (int i = nums.Length - k; i < nums.Length; i++)
        {
            int j = (i + k) % nums.Length;
            while (j < i)
            {
                int t = nums[j];
                nums[j] = nums[i];
                nums[i] = t;
                j = j + k;
            }
        }
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution3();

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 }),
    InlineData(new[] { -1, -100, 3, 99 }, 2, new[] { 3, 99, -1, -100 }),
    InlineData(new[] { 1, 2 }, 0, new[] { 1, 2 }), InlineData(new[] { -1 }, 2, new[] { -1 }),
    InlineData(
        new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, 7,
        new[] { 17, 18, 19, 20, 21, 22, 23, 11, 12, 13, 14, 15, 16 }
    )]
    public void Test(int[] input, int k, int[] expect)
    {
        So.Rotate(input, k);
        Assert.Equal(expect, input);
    }
}
public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
