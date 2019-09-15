# [8. String to Integer (atoi)](https://leetcode.com/problems/string-to-integer-atoi/)

Implement atoi which converts a string to an integer.

The function first discards as many whitespace characters as necessary until the first non-whitespace character is found. Then, starting from this character, takes an optional initial plus or minus sign followed by as many numerical digits as possible, and interprets them as a numerical value.

The string can contain additional characters after those that form the integral number, which are ignored and have no effect on the behavior of this function.

Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: `[−2^31,  2^31 − 1]`. If the numerical value is out of the range of representable values, INT_MAX (2^31 − 1) or INT_MIN (−2^31) is returned.

## Remarks

要先计算ch-'0'在与前面的加起来，否则如果先加ch可能就溢出了。
