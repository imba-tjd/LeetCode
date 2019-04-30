# [88. Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/)

Given two sorted integer arrays nums1 and nums2, merge nums2 into nums1 as one sorted array.

Note:

* The number of elements initialized in nums1 and nums2 are m and n respectively.
* You may assume that nums1 has enough space (size that is greater or equal to m + n) to hold additional elements from nums2.

## Remarks

本来以为只能每次把后面所有的向后移的，这样也太差了，适合干这事的是链表，不是数组。

看了答案发现居然可以从后往前赋值。

解法三来自：<https://leetcode.com/problems/merge-sorted-array/discuss/29515/4ms-C%2B%2B-solution-with-single-loop>
