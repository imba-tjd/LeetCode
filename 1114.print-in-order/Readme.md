# [1114. Print in Order](https://leetcode.com/problems/print-in-order/)

## Remarks

此题暂时不支持C#。

其实volatile不是用于多线程同步的，我这个代码不用volatile也能过。看起来通过指针访问是不会优化掉的？

单纯用原子操作也只能实现自旋锁；正确方式应该用`pthread.h`或`threads.h`，后者glibc2.28实现，但MinGW-w64没有。
