# [704. Binary Search](https://leetcode.com/problems/binary-search/)

Given a sorted (in ascending order) integer array nums of n elements and a target value, write a function to search target in nums. If target exists, then return its index, otherwise return -1.

## Remarks

重点在于正常的返回需要控制在循环内，而循环外直接返回-1就好。

当使用左闭右开区间时，循环条件为to > from，则循环内to至少比from大1，最少时即只有from这一个元素，此时from==midi。移动时如果mid不匹配，则midi直接赋给to，或+1后赋给from。

使用闭区间时，循环条件为to >= from，即循环内to和from可相等，此时只有一个元素，midi==from==to。如果循环条件用to>from，则to==from时跳出，在最后需要判断当前值是否等于target；这样又会引入数组为空时的特殊情况，此问题前面的方法没有因为最后一步时无需访问数组。

另外索引直接相加时可能会溢出，所以要把加变成偏移。（第一个二分查找程序于1946年就公布了,但是第一个没有bug的程序在1962年才出现）
