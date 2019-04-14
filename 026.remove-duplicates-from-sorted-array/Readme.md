# [26. Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/)

Given a sorted array nums, remove the duplicates in-place such that each element appear only once and return the new length.

Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

## Remarks

用int?注意赋值顺序，int到int?是可以隐式转化的，写成`nums[i++] = pre = nums[j];`就不行了。
