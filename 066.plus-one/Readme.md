# [66. Plus One](https://leetcode.com/problems/plus-one/)

Given a non-empty array of digits representing a non-negative integer, plus one to the integer.

The digits are stored such that the most significant digit is at the head of the list, and each element in the array contain a single digit.

You may assume the integer does not contain any leading zero, except the number 0 itself.

## Remarks

点踩的人很多，不过既然题目故意没有明说，最前面肯定是有可能进一位的。

AddOnce方法如果手动替换，耗时反而会增加，大概是因为做了多次间接引用。

耗时最少的解法还有点意思：从后往前遍历数组时，加一后如果不等于9，就直接返回，这样可以使用原数组，不必重新分配；如果等于9，就把当前位设为0，继续循环。最后如果能出数组，就分配Length+1的空间，并把第0位设为1，**无需Array.Copy**因为后面的全是0。
