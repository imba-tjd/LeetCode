# [41 First Missing Positive](https://leetcode.com/problems/first-missing-positive/) 缺失的第一个正数

Given an unsorted integer array nums, return the smallest missing positive integer.

You must implement an algorithm that runs in O(n) time and uses constant extra space.

## Remarks

如果n个数恰好是[1,n]，则答案就是n+1。如果有数字小于等于0或大于n，则[1,n]中必然缺少数字，那个数字就是答案。关键是虽然有的数字可能很大，但不用管它们。或者把所有的数字放入哈希表，再看[1,n]是否存在其中，如果不在就是答案。不过这样需要O(n)的空间。

一种不用额外空间的方法是修改原数组：先完整遍历一次nums，把0和负数设为一个大于n的数，表示不存在。第二次遍历，对于在[1,n]中的数字num，把nums[num-1]设为负数，表示存在此数字；细节上，因为遍历边修改了数组，遍历时可能在后面的数出现负数，但其对应的值又确实要处理。第三次遍历，所有的负数表示其下标的数字存在，第一个在[1,n]中的非负数就是答案。

TODO: 还有一种替换的做法。
