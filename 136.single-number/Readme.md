# [136. Single Number](https://leetcode.com/problems/single-number)

Given a **non-empty** array of integers, every element appears twice except for one. Find that single one.

## Remarks

* dic就不说了，只是如果key不存在的话就++会抛异常，所以要分别处理
* 用hashset，可以第一次见到时加进去，第二次见到时去掉
* 异或：0异或另一个数等于另一个数，所以可以把初始值声明为0，结尾也不需要异或一个0回来
