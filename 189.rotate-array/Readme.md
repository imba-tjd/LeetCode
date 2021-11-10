# [189. Rotate Array](https://leetcode.com/problems/rotate-array/) 旋转数组

Given an array, rotate the array to the right by k steps, where k is non-negative.

## Remarks

k可能大于数组长度，取余一下。

往后移动k步，相当于把最后面那部分放到前面，或者反过来理解是把前面Length-k个数放到后面。

~~每次移动一个肯定超时~~ 没超时，只是LeetCode不卡时间。

前两种做法都容易理解。第一种也可用队列。第二种是原地的，原理是A^-1B^-1==(BA)^-1。

第三种做法试图直接把数字换到指定位置，我用原位置当temp，但是结果是错的，主要是j有可能大于i；LeetCode给解释的没看懂，现在官方解答不公开了。
