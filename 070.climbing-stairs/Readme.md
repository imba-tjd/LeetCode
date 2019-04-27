# [70. Climbing Stairs](https://leetcode.com/problems/climbing-stairs/)

You are climbing a stair case. It takes n steps to reach to the top.

Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?

## Remarks

其实只要使用三个变量就好了，没必要用数组。而三变量又有两种思路，一是每次只管一个数，更新后两个数，也就是Solution2；二是每次管两个数，更新第三个数，这样速度稍快，但要特殊处理n<=2的情形。

然后忽然发现其实这就是斐波那契数列，它其实是有通项公式的，不过最终速度不一定快。
