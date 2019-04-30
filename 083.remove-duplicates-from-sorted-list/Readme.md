# [83. Remove Duplicates from Sorted List](https://leetcode.com/problems/remove-duplicates-from-sorted-list/)

Given a sorted linked list, delete all duplicates such that each element appear only once.

## Remarks

如果只用一个指针，需要进行 `head.next = head.next.next;` 这样的赋值，可能缓存不友好。

解法二使用两个指针，但是速度提升不太明显。如果把 `current` 换成直接使用 `head`，测试结果居然是最快的，感觉毫无道理啊。
