# [88. Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/) 合并两个有序数组

Given two sorted integer arrays nums1 and nums2, merge nums2 into nums1 as one sorted array.

You may assume that nums1 has enough space (size that is greater or equal to m + n) to hold additional elements from nums2.

## Remarks

解法一是复制过去后排序。

一种合并两个有序数组的方式是每次从两者中取一个较小的，但本题用nums1存放最终结果，这导致要移动大量元素。解法二从后往前每次取两者中较大的，只要一轮就行。

解法三是二的变化，利用了只需要把nums2合并进去就结束的特点。
