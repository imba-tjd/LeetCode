# 4. Median of Two Sorted Arrays

There are two sorted arrays nums1 and nums2 of size m and n respectively.

Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).

You may assume nums1 and nums2 cannot be both empty.

## Remarks

O(log (m+n))的解法过于复杂，不会，见：<https://leetcode.com/articles/median-of-two-sorted-arrays/>，但是测试下来时间并不比我的快。

本来普通的O(m+n)做法很简单的，每次去掉两个数组中最小的一个，直到下一个是中位数；如果总个数有偶数个就再取一个求平均。但是还是花了很多时间来写：

1. 中位数的索引是最后一个数的索引/2，即所有组的长度加起来**减一**除以二。
2. 因为0是有效索引，a和b必须代表**接下来评估**的位置，不能代表已评估的位置。
3. 可以用合并排序中的方式简单优化前一部分。
4. 因为数组越界会抛异常，不是null，并不能用?[]简单解决。
