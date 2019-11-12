# [142. Linked List Cycle II](https://leetcode.com/problems/linked-list-cycle-ii/)

问题描述基本与 [141. Linked List Cycle](../141.linked-list-cycle) 相同。只是这次需要返回环的起点，而不仅仅是判断有没有。

## Remarks

哈希的方式几乎和第一种一样。

快慢指针法（Floyd 算法）：关键是相遇的时候不是环的开头，要进行一些处理：https://leetcode-cn.com/problems/linked-list-cycle-ii/solution/linked-list-cycle-ii-kuai-man-zhi-zhen-shuang-zhi-/

另外还需要考虑长度小于3的处理，因为第一步中的fast就已经走到第三个结点了（一开始两个指针都指向有效的第一个元素）。
