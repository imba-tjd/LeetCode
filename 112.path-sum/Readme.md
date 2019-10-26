# [112. Path Sum](https://leetcode.com/problems/path-sum/)

Given a binary tree and a sum, determine if the tree has a root-to-leaf path such that adding up all the values along the path equals the given sum.

Note: A leaf is a node with no children.

## Remarks

不能把最后一句写成 `root.val == sum ? root.left == null && root.right : ...` **且**放到第二句，那样会导致递归到非叶子结点sum和val相等时返回false，但其实应该继续往下递归。要不就写成 `root.left == null && root.right == null ? root.val == sum : ...`。
