# [189. Rotate Array](https://leetcode.com/problems/rotate-array/)

Given an array, rotate the array to the right by k steps, where k is non-negative.

## Remarks

每次移动一个肯定超时。前两种做法都容易理解。

第三种做法是直接把数字换到指定位置，我用原位置当temp，但是结果是错的，主要是j有可能大于i；LeetCode给解释的没看懂。
