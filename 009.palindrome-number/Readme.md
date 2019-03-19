# [9. Palindrome Number](https://leetcode.com/problems/palindrome-number)

Determine whether an integer is a palindrome. An integer is a palindrome when it reads the same backward as forward.

## Remarks

* 小于0直接返回false
* 用字符串很简单，不过需要一定空间
* 因为直接转换数字可能发送溢出，可以使用long
* 如果要用int，可以取后一半的数字跟前一半的比较；**当后一半数字大于前一半时说明达到边界了**
