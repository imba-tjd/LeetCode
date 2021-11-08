# [26. Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/)

Given a sorted array nums, remove the duplicates **in-place** such that each element appear only once and return the new length.

返回修改后的数组长度。

## Remarks

快慢指针：一个一直扫描，另一个指示待赋值位置。但是需要判断什么时候发生变化。

一种方式是以快指针为中心，再用一个变量记录它前一个的值。此时i就是真正的待赋值位置。用int?注意赋值顺序，int到int?是可以隐式转化的，不能写成`nums[i++] = pre = nums[j];`。

第二种方式是用慢指针记录当前值，当不等时先把慢指针移动到下一个位置，再赋值。因为慢指针初始值是0，隐含处理过了第一个位置，导致数组长度为0时必须单独处理。
