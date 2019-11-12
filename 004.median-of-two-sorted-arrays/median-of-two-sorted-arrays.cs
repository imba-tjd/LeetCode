using System;
using Xunit;

namespace Problems.Problem004.MedianofTwoSortedArrays
{
    public interface ISolution { double FindMedianSortedArrays(int[] nums1, int[] nums2); }

    abstract class Solution : ISolution
    {
        protected abstract void Move(int[] a, ref int ai, int[] b, ref int bi, int half);
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int ai = 0, bi = 0, half = (nums1.Length + nums2.Length - 1) / 2;

            Move(nums1, ref ai, nums2, ref bi, half);

            double median = ExtractMin(nums1, ref ai, nums2, ref bi);
            if ((nums1.Length + nums2.Length) % 2 == 0)
                median = (median + ExtractMin(nums1, ref ai, nums2, ref bi)) / 2;
            return median;
        }

        // 从两个数组中取出较小的一个，并递增索引
        protected int ExtractMin(int[] a, ref int ai, int[] b, ref int bi)
        {
            if (ai < a.Length && bi < b.Length)
                if (a[ai] < b[bi])
                    return a[ai++];
                else
                    return b[bi++];
            else if (ai < a.Length)
                return a[ai++];
            else
                return b[bi++];
        }
    }

    class Solution1 : Solution
    {
        protected override void Move(int[] a, ref int ai, int[] b, ref int bi, int half)
        {
            while (--half >= 0)
                ExtractMin(a, ref ai, b, ref bi);
        }
    }

    class Solution2 : Solution
    {
        protected override void Move(int[] a, ref int ai, int[] b, ref int bi, int half)
        {
            while (ai < a.Length && bi < b.Length && ai + bi < half)
                if (a[ai] < b[bi]) ai++;
                else bi++;
            while (ai < a.Length && ai + bi < half)
                ai++;
            while (bi < b.Length && ai + bi < half)
                bi++;
        }
    }

    class Solution3 : Solution
    {
        protected override void Move(int[] a, ref int ai, int[] b, ref int bi, int half)
        {
            for (int i = 0; i < half; i++)
                if (ai >= a.Length)
                    bi++;
                else if (bi >= b.Length)
                    ai++;
                else if (a[ai] > b[bi])
                    bi++;
                else
                    ai++;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution1();

        [Theory]
        [InlineData(new[] { 1, 3 }, new[] { 2 }, 2.0)]
        [InlineData(new[] { 1, 3 }, new int[0], 2.0)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 4, 5 }, 3.0)]
        [InlineData(new[] { 1, 2 }, new[] { 3, 4 }, 2.5)]
        [InlineData(new[] { 3, 4 }, new[] { 1, 2 }, 2.5)]
        [InlineData(new[] { 1, 2, 3 }, new int[0], 2.0)]
        public void Test(int[] nums1, int[] nums2, double expect)
        {
            var so = GetSo;
            var result = so.FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution1(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { override protected ISolution GetSo => new Solution3(); }
}
