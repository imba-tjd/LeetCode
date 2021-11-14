# [20. Valid Parentheses](https://leetcode.com/problems/valid-parentheses) 有效的括号

Given a string containing just the characters `'('`, `')'`, `'{'`, `'}'`, `'['` and `']'`, determine if the input string is valid.

Note that an empty string is also considered valid.

## Remarks

最正常的做法是用栈。另一种低效但也行的做法是不断替换成对的括号成空字符串，直到那一轮替换后长度不变即没替换，最后长度为零就算有效的。
