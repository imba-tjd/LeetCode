# [169. Majority Element](https://leetcode.com/problems/majority-element/) 主元素查找

Given an array of size n, find the majority element. The majority element is the element that appears more than `⌊ n/2 ⌋` times.

## Remarks

第一眼看上去是求出现次数最多的，但实际上主元素要求数量超过1/2，因此可以通过排序后取length/2解决，O(nlogn)。

普通的O(n)方法是用dict记录，遇到次数超过1/2的就找到了。

最好的算法：把count==0时遇到的元素当作主元素，之后如果遇到相同的“主元素”，就count++，否则count--。原理是主元素最差时也比其它数多1，遇到主元素时计数+1，遇到其它数时计数-1，这样可以把其它数“消除掉”；count==0时需要更新主元素，前面的可以都不要了。

此题目一定存在主元素。若是可能不存在，取length/2就不行了：dict算法不能提前返回，全添加后找出最大的看是否超过length/2。最好的算法要重新遍历一遍，对找到的元素计数。

若允许不准确，也可随机取，因为每次取都有至少1/2的几率取到，且数量大时这样快得多。
