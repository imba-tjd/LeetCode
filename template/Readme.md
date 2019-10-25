# Code Template

Try to DRY creating practice files.

* Usage: `Gen.exe [/f] 1. Two Sum`

Note: The CLI must be *cmd* because parentheses are special characters in PS and Bash. Adding quotation marks can solve this but it's a burden.

## 思路

分为两个部分：解析输入，和根据要求创建文件。

输入分为三个部分：选项、序号、剩余部分。需要研究如何方便地增加选项，以及在不使用反射的情况下把选项和解析出来的东西联系起来。

文件，可能需要include这样的特性。现在生成和树有关的模板用的是手动复制的。

## Test Cases

Problem|URL Suffix
-|-
Sqrt(x)|sqrtx
Pow(x, n)|powx-n
Implement strStr()|implement-strstr
String to Integer (atoi)|string-to-integer-atoi
Implement Trie (Prefix Tree)|implement-trie-prefix-tree
