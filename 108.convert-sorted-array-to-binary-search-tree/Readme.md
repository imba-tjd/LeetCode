# [108. Convert Sorted Array to Binary Search Tree](https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/)

Given an array where elements are sorted in ascending order, convert it to a height balanced BST.

Input: [-10,-3,0,5,9]

One possible answer is:

```
      0
     / \
   -3   9
   /   /
 -10  5
```

## Remarks

这其实是最佳AVL树的算法，思想类似于二分搜索，LC的例子要用左闭右开区间，其实闭区间也是可以的。最后形成的效果是一个只有最后一层不满的AVL树。
