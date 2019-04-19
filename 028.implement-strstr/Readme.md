# [28. Implement strStr()](https://leetcode.com/problems/implement-strstr/)

Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

What should we return when needle is an empty string? This is a great question to ask during an interview.For the purpose of this problem, we will return 0 when needle is an empty string. This is consistent to C's `strstr()` and Java's `indexOf()`.

## Remarks

BF算法，因为需要内层循环完成后才能判断是否成功，第一想法是加个bool变量。后来看了别人的解析发现可以用j的位置判断。

KMP算法待添加。
