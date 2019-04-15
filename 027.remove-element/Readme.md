# [27. Remove Element](https://leetcode.com/problems/remove-element/)

Given an array nums and a value val, remove all instances of that value in-place and return the new length.

Note that the order of those elements can be arbitrary.

## Remarks

理论上最优的方法是，从左往右找一个等于val的，从右往左找一个不等于val的，右边的赋给左边的，然后p++，q--。边界为`p<q`，则可能p==q或q+1==p；p是找到了（即q找不到）还是待评估又是两种情况。总之问题就是第一个子循环的跳出条件过多，导致后续状态过多。虽然我的Solution1通过了，但`nums[q] = val;`这句相当于做了交换，导致p++，q--可以去掉。

解答二使用快慢指针法：快指针找!=val的，赋值给慢指针，遇到==val时直接++；慢指针只有复制后才++。相比于第二种方法，这样可能会多很多赋值，比如`[1,0,0,0,0],1`，理论上只要赋值一次就好，此方法会赋值4次。但实际上这种方法不仅写起来简单，速度也是最快的。

解法三与一类似，只是每次循环只改变一个指针，这样就不会出现交错的情况，同时可以避免多余的赋值。但是这样速度反而更慢，大概是缓存不友好吧。而且最后还是要判断p的值。

解法四是三的变化，最大的不同是q的初始值改成了Length，这样可以省去许多额外的判断。另外q指向的数等于val时仍会发生赋值，不过下一轮的p是不变的，所以还是q动。
