# [137. Single Number II](https://leetcode.com/problems/single-number-ii)

Given a non-empty array of integers, every element appears three times except for one, which appears exactly once. Find that single one.

## Remarks

> https://leetcode.com/problems/single-number-ii/discuss/167343/topic
>
> PS.以下的a和b与文章互换了。

数字最多出现两次的时候，清空的公式是 a = a xor n

如果数字能出现三次的话，香农定理，需要用两个变量进行记录：

1. 当5第一次出现的时候，a = 5, b=0，a记录这个数字
2. 当5第二次出现的时候，a = 0, b=5，b记录了这个数字
3. 当5第三次出现的时候，a = 0, b=0，都清空了

即如果某个数字n出现了一次，就存在a中，出现了两次，就存在b中。如果要求出现一次或两次的唯一一个数字就返回 a|b

与最多出现两次的区别：出现第三次时，要让a仍然等于零。此时上一步的b含有n，用 n & ~b 可以去掉n；或者也可也理解成0^n等于n，再&~n；而b类似，不过是受本步的a的影响。

因为异或满足交换律和结合律（？没看懂），所以不用担心不在一起，也所以括号加在^还是&都正确；不加时&优先级更高

```
foreach n in nums
   a = a ^ n & ~b
   b = b ^ n & ~a
return a
```
