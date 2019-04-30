# [268. Missing Number](https://leetcode.com/problems/missing-number)

Given an array containing n distinct numbers taken from `0, 1, 2, ..., n`, find the one that is missing from the array.

## Remarks

* 方法1：排序，看index和value不一样的地方。时间复杂度O(nlgn)，空间复杂度O(1)
* 方法2：哈希表，建好后再遍历找不存在的
* 方法3：利用异或，把index和value异或，如果一样就会为0；如果中间出现了少了的数，仅仅只是错开一位，之后就会消掉。如果全部符合，需要返回Length，所以一开始的值要是Length
* 方法4：等差数列求和，减去数组所有元素的和
