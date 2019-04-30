using System;
using System.Collections.Generic;
using Xunit;

namespace MergeSortedArray
{
    public interface ISolution { void Merge(int[] nums1, int m, int[] nums2, int n); }
    class Solution : ISolution
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            Array.Copy(nums2, 0, nums1, m, n);
            Array.Sort(nums1, 0, m + n);
        }
    }
    class Solution2 : ISolution
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            for (int dest = m + n - 1; dest >= 0; dest--)
                if (m > 0 && n > 0)
                    nums1[dest] = nums1[m - 1] > nums2[n - 1] ? nums1[--m] : nums2[--n];
                else if (m == 0)
                    nums1[dest] = nums2[--n];
                else
                    nums1[dest] = nums1[--m];
        }
    }
    class Solution3 : ISolution
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int i = m - 1, j = n - 1, tar = m + n - 1;
            while (j >= 0)
                nums1[tar--] = i >= 0 && nums1[i] > nums2[j] ? nums1[i--] : nums2[j--];
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution3();

        [Theory]
        [InlineData(new[] { 1, 2, 3, 0, 0, 0 }, 3, new[] { 2, 5, 6 }, 3, new[] { 1, 2, 2, 3, 5, 6 })]
        public void Test(int[] nums1, int m, int[] nums2, int n, int[] expect)
        {
            var so = GetSo;
            so.Merge(nums1, m, nums2, n);
            Assert.Equal(expect, nums1);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { override protected ISolution GetSo => new Solution3(); }
}
