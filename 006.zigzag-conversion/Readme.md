# [6. ZigZag Conversion](https://leetcode.com/problems/zigzag-conversion/)

Input: s = "PAYPALISHIRING", numRows = 4\
Output: "PINALSIGYAHRPI"

```
P     I    N
A   L S  I G
Y A   H R
P     I
```

## Remarks

### 方法一

最容易想到的是建立row个string，每次取一个字符放进去，用一个变量控制向下移动还是向上移动。

因为当row为1时，第0行既是上边界也是下边界，如果直接向下移动就会超出去，所以直接返回s算了。

### 方法二

直接按输出的顺序计算，外围循环是numRows，而内部是计算k。

对于第0行，每个字符是`k*(numRows*2-2)`；对于第row行，则是`k*(numRows*2-2)+row`（对第0行也适用）与`(k+1)*(numRows*2-2)-row`（即第0行的下一个往回数）。

但最后一行时也只有一个且第一个数对第0行也适用，所以后者的条件还要加上row!=0&&row!=numRows-1。

另外还需考虑组不全时数字不存在的情况，非第0行的两个数都可能发生。

因为当row为1时也要单独写出来，不可省略。
