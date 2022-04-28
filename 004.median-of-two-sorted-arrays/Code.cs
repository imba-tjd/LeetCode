namespace LeetCode.Problems.P004MedianofTwoSortedArrays;

public interface ISolution { double FindMedianSortedArrays(int[] nums1, int[] nums2); }

class Solution : ISolution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        Span<int> a = nums1.AsSpan(), b = nums2.AsSpan();
        int half = (nums1.Length + nums2.Length - 1) / 2;

        while (--half >= 0) // 等价于for(i=0;i<half;i++)，因此执行完后再取一次才是median
            ExtractMin(ref a, ref b);

        double median = ExtractMin(ref a, ref b);
        if ((nums1.Length + nums2.Length) % 2 == 0)
            return (median + ExtractMin(ref a, ref b)) / 2;
        else
            return median;
    }

    // 从两个数组中取出较小的一个，并递增索引
    int ExtractMin(ref Span<int> a, ref Span<int> b)
    {
        int n;
        if (a.Length == 0)
        {
            // 若用指针可一句 n = *p++
            n = b[0];
            b = b.Slice(1);
        }
        else if (b.Length == 0)
        {
            n = a[0];
            a = a.Slice(1);
        }
        else if (a[0] < b[0])
        {
            n = a[0];
            a = a.Slice(1);
        }
        else
        {
            n = b[0];
            b = b.Slice(1);
        }
        return n;
    }
}

// abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution();

    [Theory]
    [InlineData(new[] { 1, 3 }, new[] { 2 }, 2.0)]
    [InlineData(new[] { 1, 3 }, new int[0], 2.0)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 4, 5 }, 3.0)]
    [InlineData(new[] { 1, 2 }, new[] { 3, 4 }, 2.5)]
    [InlineData(new[] { 3, 4 }, new[] { 1, 2 }, 2.5)]
    [InlineData(new[] { 1, 2, 3 }, new int[0], 2.0)]
    public void Test(int[] nums1, int[] nums2, double expect)
    {
        var result = So.FindMedianSortedArrays(nums1, nums2);
        Assert.Equal(expect, result);
    }
}
// public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
