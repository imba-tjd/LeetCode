# [4. Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) 寻找两个正序数组的中位数

There are two sorted arrays nums1 and nums2 of size m and n respectively.

Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).

You may assume nums1 and nums2 cannot be both empty.

## Remarks

普通的O(m+n)思路：每次去掉两个数组中最小的一个，直到取到中位数；如果总个数有偶数个就再取一个求平均。中位数的索引是长度**减一**除以二，因为当长度是偶数如4时4/2==2，但实际应选索引为1和2的两个数的平均。

O(log (m+n))解法的基本思想：

1. 分别求nums1和nums2的中位数，记作a和b。
2. 若a==b，则a或b即为所求中位数。
3. 若a<b，舍弃nums1中较小的一半，同时舍弃nums2中较大的一半，要求两次舍弃的长度相等。
4. 若a>b，舍弃nums1中较大的一半，同时舍弃nums2中较小的一半，要求两次舍弃的长度相等。
5. 在3和4保留的两个升序序列中，重复234，直到两个序列中均只含一个元素为止。

这是人能想出来的解法吗？而且这还不是最佳的，这用了二分的思想。最佳的解法需要理解“中位数的作用是什么”，在统计中，中位数被用来：将一个集合划分为两个长度相等的子集，其中一个子集中的元素总是大于另一个子集中的元素。
