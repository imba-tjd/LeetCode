# [14. Longest Common Prefix](https://leetcode.com/problems/longest-common-prefix/)

Write a function to find the longest common prefix string amongst an array of strings.

## Remarks

* 没办法二分，必须一个个比较。因为二分只能二分纵坐标，字符串比较时还是会从头开始
* 尝试了一下多线程，时间反而更长了，不过也是意料之中。internal方法是因为不这样做每次递归都会试图建立两个线程
* 为了复用第一种方法，试了一下反射，而且划分两个子数组也花了许多时间
* Invoke方法时，本来直接传的string数组，结果被隐式转换成了object数组；运行时报无法把string转换成object，其实是把每个string都当作函数单独的参数了
* slice的长度那里也可以写Length - Length/2，如果要用加法就要路灯原理了
