# [169. Majority Element](https://leetcode.com/problems/majority-element/)

Given an array of size n, find the majority element. The majority element is the element that appears more than `⌊ n/2 ⌋` times.

## Remarks

注意虽然也是出现次数最多的，但题目实际上求的是数量超过1/2的，因此可以通过排序后返回length/2解决。不过速度不算太快。

普通的O(n)方法是用dic记录，遇到次数超过1/2的直接返回就好了，同上也不需要添加完后再去遍历一遍。

还可以使用随机数的方法，因为每次取都有至少1/2的几率取到。虽然产生随机数也有开销，但数量大时反而快得多。

最好的算法：把count==0时遇到的元素当作主要数，之后如果遇到相同的“主要数”，就count++，否则count--。原理是主要数最差时也比其它数多1，遇到主要数时计数+1，遇到其它数时计数-1，这样可以把其它数“消除掉”；但count==0时需要更新主要数，前面的可以都不要了。
