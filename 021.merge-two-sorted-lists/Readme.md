# [21. Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/)

Merge two sorted linked lists and return it as a new list. The new list should be made by splicing together the nodes of the first two lists.

## Remarks

* 因为原本的列表是链表，只要改指针就好了，无需全部生成新的
* 单元测试的参数无法是new出来的自定义对象；这种情况也应该主动避免，否则会太复杂
* 判断两个数组相等不要用二重循环
