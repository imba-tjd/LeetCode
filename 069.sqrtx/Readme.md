# [69. Sqrt(x)](https://leetcode.com/problems/sqrtx/)

Implement int sqrt(int x).

Compute and return the square root of x, where x is guaranteed to be a non-negative integer.

Since the return type is an integer, the decimal digits are truncated and only the integer part of the result is returned.

## Remarks

直接从头遍历会超时，正常。

二分法，32位int的最大值是2^31-1，开方后是 46340.950001052，用左闭右开，所以to为46341。测试结果居然耗时和内存都超过100%的C#提交，简直不敢相信。

### 牛顿法

令 f(x) = x^2 - a，它的一个正实根就是sqrt(a)。随机取一点t（实现中t=a），做该点在函数上的切线方程 Y-f(t) = 2t(X-t)，令Y=0，得到的 X = t - f(t)/(2t) 是比t更接近根的迭代值。再代入f(t)=t^2-a，化简得(t+a/t)/2，作为下一次迭代的t

```c
float SqrtByNewton(float x) {
    float val = x; // 最终值
    float last; // 保存上一个计算的值
    do {
        last = val;
        val = (val + x/val) / 2;
    } while (abs(val-last) > EPS);
    return val;
}
```
