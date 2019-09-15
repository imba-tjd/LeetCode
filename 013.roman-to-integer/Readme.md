# [13. Roman to Integer](https://leetcode.com/problems/roman-to-integer/)

Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.

## Remarks

我的做法是当前和之前的比较，就要反过来减两遍。如果是看当前与下一个的大小就不会这样，但是最后一个就直接加就行了，否则会越界。
