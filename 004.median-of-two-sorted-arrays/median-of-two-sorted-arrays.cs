using System;
using Xunit;

namespace MedianofTwoSortedArrays
{
    class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int a = 0, b = 0, half = (nums1.Length + nums2.Length - 1) / 2;

            // 注释掉的能略微优化一点
            // while (a < nums1.Length && b < nums2.Length && a + b < half)
            //     if (nums1[a] < nums2[b]) a++;
            //     else b++;
            // while (a < nums1.Length && a + b < half)
            //     a++;
            // while (b < nums2.Length && a + b < half)
            //     b++;
            while (--half >= 0)
                ExtractMin(nums1, ref a, nums2, ref b);

            double median = ExtractMin(nums1, ref a, nums2, ref b);
            if ((nums1.Length + nums2.Length) % 2 == 0)
                median = (median + ExtractMin(nums1, ref a, nums2, ref b)) / 2;
            return median;
        }
        int ExtractMin(int[] a, ref int ai, int[] b, ref int bi)
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

    public class UnitTest
    {
        [Theory]
        [InlineData(new[] { 1, 3 }, new[] { 2 }, 2.0)]
        [InlineData(new[] { 1, 3 }, new int[0], 2.0)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 4, 5 }, 3.0)]
        [InlineData(new[] { 1, 2 }, new[] { 3, 4 }, 2.5)]
        [InlineData(new[] { 3, 4 }, new[] { 1, 2 }, 2.5)]
        void Test(int[] nums1, int[] nums2, double median)
        {
            var solution = new Solution();
            double result = solution.FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(median, result);
        }
    }
}

