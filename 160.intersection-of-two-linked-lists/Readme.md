# [160 Intersection of Two Linked Lists](https://leetcode.com/problems/intersection-of-two-linked-lists/) 相交链表

返回两个链表的第一个相交的结点，如果没相交则返回null。因为是单链表，相交后就都是同一个结点了，即只可能是Y型，不可能是X型。

另外，LeetCode要求判断的是对象相等，而非值相等。因此不方便构造测试用例

## Remarks

第一种做法：先分别计算出二者的长度，让长的那一个先走超出的长度，之后再一起走。因为如果有相交的地方，最底部肯定是对齐的。

第二种做法：两个指针都同时往下走，当短的先走到底部为null时，转移到长的那条链上；长的同理。原理是短的会走m+n个结点，长的会走n+m个结点，二者相等。
