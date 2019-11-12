# [203. Remove Linked List Elements](https://leetcode.com/problems/remove-linked-list-elements/)

Remove all elements from a linked list of integers that have value val.

## Remarks

解法一是从前往后迭代处理，但必须对current.next.val与val进行比较，这样才能改变current的next字段。又可能出现全部都要移除的情况，就需要一个额外头节点；要不就在最后再单独判断一下head.val如果等于val就返回null。

解法二的递归解法来自：<https://leetcode.com/problems/remove-linked-list-elements/discuss/57306/3-line-recursive-solution>。无法写成尾递归的形式，必须从后往前处理，因为最后是返回自己。如果全部都要移除，此代码也能正常工作，无需头节点。
