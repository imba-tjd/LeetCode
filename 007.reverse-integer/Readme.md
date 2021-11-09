# [7. Reverse Integer](https://leetcode.com/problems/reverse-integer/)

Given a 32-bit signed integer, reverse digits of an integer.

## Remarks

* 不同语言对出现负数时的取余可能不同，C和C#仅取决于被除数的符号
* 输入在int范围内，但反过来以后就可能会溢出了
* 如果第一步就用abs去负号，输入最小的int时就会抛异常
