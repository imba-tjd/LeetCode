# [27. Remove Element](https://leetcode.com/problems/remove-element/)

Given an array nums and a value val, remove all instances of that value in-place and return the new length.

Note that the order of those elements can be arbitrary.

## Remarks

解法一从左往右找一个等于val的，与右边的指针交换，交换前右边的可能等于val也可能不等，因此仅q--，不能p++，等到下一轮再评估。当两指针相遇时右边的还是可能是val也可能不是，因此要判断一下才能返回长度。这样会改变元素原顺序。

若从右往左找一个不等于val的，两者交换，p++，q--，则可能导致p超过q。边界条件过多，非常难写正确。

解法二使用快慢指针法：快指针找不等于val的，赋值给慢指针，遇到==val时直接++；慢指针赋值后才++。相比于第一种方法，这样可能多很多赋值，比如`[1,0,0,0,0],1`，理论上只要赋值一次就好，此方法会赋值4次。

解法三是一的变化，q的初始值改成了Length，且一轮循环只动一个指针，可以省去许多额外的判断。

解法四是记录扫描到了多少个等于val的，把所有不等于val的往前移Length-n个位置。

解法五是四的变化，记录扫描到了多少个不等于val的(即要保留的)，把它作为下标，把快指针赋值给它。
