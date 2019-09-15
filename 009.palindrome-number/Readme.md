# [9. Palindrome Number](https://leetcode.com/problems/palindrome-number)

Determine whether an integer is a palindrome. An integer is a palindrome when it reads the same backward as forward.

## Remarks

* 小于0直接返回false
* 用字符串很简单，整个反转或者两边往中间靠都可以，不过需要一定空间
* 把整个数字反转后和原数字比较，如果原数字就不是回文数，可能会溢出。要么使用long，要么catch后返回false
* 还可以取后一半的数字跟前一半的比较，**当后一半数字大于前一半时说明达到边界了**。但是必须处理长度奇数和`10`这样的特殊情况
