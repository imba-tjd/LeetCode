# [141. Linked List Cycle](https://leetcode.com/problems/linked-list-cycle/)

Given a linked list, determine if it has a cycle in it.

To represent a cycle in the given linked list, we use an integer `pos` which represents the position (0-indexed) in the linked list where tail connects to. If `pos` is -1, then there is no cycle in the linked list.

## Remarks

如果有个结点指向了之前的结点，则存在循环。一种方法自然是HashSet。

第二种方法是快慢指针，如果快指针与慢指针相等，说明存在循环；如果快指针为null，说明无循环（快指针一定先变成null）。其实这个又有两种做法，一是把快指针为null作为跳出条件，指针相等放在函数体里；第二种是反过来。两者差不多。C#的可空运算符简化了一定的代码。
