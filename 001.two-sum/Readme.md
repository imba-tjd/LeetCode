# [1. Two Sum](https://leetcode.com/problems/two-sum/) 两数之和

Given an array of integers, return **indices** of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

## Remarks

* 可能出现两个相同但不是答案的值，所以要用TryAdd；用`dic[nums[i]] = i`耗时反而更多
