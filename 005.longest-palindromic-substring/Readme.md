# [5. Longest Palindromic Substring](https://leetcode.com/problems/longest-palindromic-substring)

Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

## Remarks

* 方法一：暴力。生成所有的子串（共nC2种选择，O(n^2)），检查是否是回文串O(n)
* 方法二：中心扩展。当一个串已经是回文串，若两端字符一致则可扩展。外层遍历一遍字符串，分别以 每个字符为中心（已是回文传）、两个字符为中心（需检查是否是回文串） 向两端扩展
* DP：使用dp[i][j]=true表示s[i..j]闭区间为回文串。初始化dp[i][i]=true、dp[i][i+1]=s[i]==s[i+i]。状态转移方程：dp[i][j] = dp[i+i][j-1] && s[i]==s[1]，过程中记录max_len = j-i+1。方法二是本方法的特例，本方法比它更差，时间复杂度一样，空间更多
* Manacher 马拉车算法：复杂度O(n)，专门用于解决本问题
