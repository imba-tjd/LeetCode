# [67. Add Binary](https://leetcode.com/problems/add-binary/)

Given two binary strings, return their sum (also a binary string).

The input strings are both non-empty and contains only characters 1 or 0.

## Remarks

居然 faster than 100% of C# submission，惊了。

解法二肯定是不可以的，long只能存64位的二进制数。

另一种方式是append后ToCharArray、Array.Reverse、new string，最终耗时一样。
