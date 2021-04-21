# Code Template

DRY creating code files.

Usage: `Gen.exe 1. Two Sum`. It uses `.t` template file to generate corresponding folder, cs, readme.

There is no flags. If the folder already exists, it simply fails.

Note: The shell **must be cmd**, because parentheses are special characters in PS and Bash. Adding quotation marks can solve this but it's a burden, so this tool is designed to parse multiple args and can't accept one arg that contains quotation marks.

## Test Cases

Problem|URL Suffix
-|-
Sqrt(x)|sqrtx
Pow(x, n)|powx-n
Implement strStr()|implement-strstr
String to Integer (atoi)|string-to-integer-atoi
Implement Trie (Prefix Tree)|implement-trie-prefix-tree
