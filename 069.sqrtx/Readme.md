# [69. Sqrt(x)](https://leetcode.com/problems/sqrtx/)

Implement int sqrt(int x).

Compute and return the square root of x, where x is guaranteed to be a non-negative integer.

Since the return type is an integer, the decimal digits are truncated and only the integer part of the result is returned.

## Remarks

直接从头遍历会超时，正常。

二分法，32位int的最大值是2^31-1，开方后是 46340.950001052，用左闭右开，所以to为46341。测试结果居然耗时和内存都超过100%的C#提交，简直不敢相信。

牛顿法待补充。
