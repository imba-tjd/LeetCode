# [100. Same Tree](https://leetcode.com/problems/same-tree/)

Given two binary trees, write a function to check if they are the same or not.

Two binary trees are considered the same if they are structurally identical and the nodes have the same value.

## Remarks

注意终止条件，即两者都为null时不要再递归调用了。

第二项可以写`a==null||b==null`，因为第一项已经处理了都为null的情况，这一项就必定一个为null一个不为null。
