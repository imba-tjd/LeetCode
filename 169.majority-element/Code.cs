namespace LeetCode.Problems.P169MajorityElement;

public interface ISolution { int MajorityElement(int[] nums); }
class Solution : ISolution
{
    public int MajorityElement(int[] nums)
    {
        var dic = new Dictionary<int, int>(nums.Length / 2 + 1);

        foreach (int n in nums)
            if (!dic.TryAdd(n, 1))
                if (++dic[n] > nums.Length / 2)
                    return n; // 无需等完全添加完后再去遍历最大的

        // throw new NotSupportedException(); // 不可能发生
        return nums[0];
    }
}

class Solution2 : ISolution
{
    public int MajorityElement(int[] nums)
    {
        Array.Sort(nums);
        return nums[nums.Length / 2];
    }
}

// Boyer-Moore Voting Algorithm
class Solution3 : ISolution
{
    public int MajorityElement(int[] nums)
    {
        int me = 0;
        int count = 0;
        foreach (int n in nums)
        {
            if (count == 0)
            {
                me = n;
                count = 1;
            }
            else if (me == n)
                count++;
            else count--;
        }
        return me;
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution GetSo => new Solution2();

    [Theory]
    [InlineData(new[] { 3, 2, 3 }, 3), InlineData(new[] { 2, 2, 1, 1, 1, 2, 2 }, 2), InlineData(new[] { 1 }, 1)]
    public void Test(int[] input, int expect)
    {
        var so = GetSo;
        var result = so.MajorityElement(input);
        Assert.Equal(expect, result);
    }
}
public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
public class Test3 : MultiTest { protected override ISolution GetSo => new Solution3(); }
