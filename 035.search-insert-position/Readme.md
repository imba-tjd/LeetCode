# [35. Search Insert Position](https://leetcode.com/problems/search-insert-position/)

Given a sorted array and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.

## Remarks

方法一是暴搜，第一个比target大的就是合适的位置。

方法二是二分搜索，毕竟是排序好的数组。Related to [704. Binary Search](../704.binary-search).<br>
使用左闭右开区间，当最后只剩一个元素时，仍在循环内，如果符合，就返回；不等于则下一步from==to，集合变空出循环，此时from一定是合适的位置，不需要再访问数组。
