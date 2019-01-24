# 1. Two Sum

Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

## Remarks

* 可能出现两个相同但不是答案的值，所以要用TryAdd
* 如果使用dic[nums[i]] = i;，耗时反而会增加……
