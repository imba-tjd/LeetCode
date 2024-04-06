# [5. Longest Palindromic Substring](https://leetcode.com/problems/longest-palindromic-substring)

Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

## Remarks

* 暴力：生成所有的子串，检查是否是回文串。有nC2种选择
* DP：如果一个串已经是回文串，只需检查两边的字符是否一致就可以扩展。初始有两种可能：单个的字符一定是回文串，两个字符则只是可能是
