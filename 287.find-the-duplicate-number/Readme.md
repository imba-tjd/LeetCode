# [287. Find the Duplicate Number](https://leetcode.com/problems/find-the-duplicate-number/)

Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive), prove that at least one duplicate number must exist. Assume that there is only one duplicate number, find the duplicate one.

Note:

1. You must not modify the array (assume the array is read only).
2. You must use only constant, O(1) extra space.
3. Your runtime complexity should be less than O(n2).
4. There is only one duplicate number in the array, but it could be repeated more than once.

## Remarks

n+1个数刚好在1到n之间。如果最多重复一次，重复的那个数也必定在1到n之间，那所有求和后减去1到n求和就好。然而此题可以重复不止一次。

普通的三种方法就略了，都不满足题目的额外要求。

答案给的是 Floyd's Tortoise and Hare，类似于 [142. Linked List Cycle II](../142.linked-list-cycle-ii) 的检测循环算法。关键是要把值当作下一次的索引，而不是对普通的索引用快慢指针；这样如果存在环就一定会相遇。

解法五来自：<https://zhuanlan.zhihu.com/p/76698113>，时间复杂度O(nlogn)，因为二分了n。
