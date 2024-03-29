namespace LeetCode.Problems.P027RemoveElement;

public interface ISolution { int RemoveElement(int[] nums, int val); }
class Solution : ISolution
{
    public int RemoveElement(int[] nums, int val)
    {
        if (nums.Length == 0)
            return 0;

        int p = 0, q = nums.Length - 1;
        while (true)
        {
            while (p < q && nums[p] != val)
                p++;
            if (p == q)
                return nums[p] == val ? p : p + 1;

            nums[p] = nums[q--];
        }
    }
}

class Solution2 : ISolution
{
    public int RemoveElement(int[] nums, int val)
    {
        int p = 0;
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] != val)
                nums[p++] = nums[i];
        return p;
    }
}

class Solution3 : ISolution
{
    public int RemoveElement(int[] nums, int val)
    {
        int p = 0, q = nums.Length;
        while (p < q)
            if (nums[p] != val)
                p++;
            else
                nums[p] = nums[--q];
        return q;
    }
}

class Solution4 : ISolution
{
    public int RemoveElement(int[] nums, int val)
    {
        int n = 0;
        for (int i = 0; i< nums.Length; i++)
            if (nums[i] != val)
                nums[i-n] = nums[i];
            else
                n++;
        return nums.Length - n;
    }
}

class Solution5 : ISolution
{
    public int RemoveElement(int[] nums, int val)
    {
        int k = 0;
        for (int i = 0; i< nums.Length; i++)
            if (nums[i] == val)
                nums[k++] = nums[i];
        return k;
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution();

    [Theory]
    [InlineData(new[] { 3, 2, 2, 3 }, 3, 2), InlineData(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5),
    InlineData(new[] { 1 }, 1, 0), InlineData(new[] { 1 }, 0, 1), InlineData(new int[0], 1, 0),
    InlineData(new[] { 3, 3 }, 3, 0), InlineData(new[] { 1, 1, 0 }, 1, 1), InlineData(new[] { 1, 0 }, 1, 1)]
    public void Test(int[] input, int val, int expect)
    {
        var result = So.RemoveElement(input, val);
        Assert.Equal(expect, result);
    }
}
public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
public class Test3 : MultiTest { protected override ISolution So => new Solution3(); }
public class Test4 : MultiTest { protected override ISolution So => new Solution3(); }
public class Test5 : MultiTest { protected override ISolution So => new Solution3(); }
