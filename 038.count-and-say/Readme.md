# [38. Count and Say](https://leetcode.com/problems/count-and-say/)

The count-and-say sequence is the sequence of integers with the first five terms as following:

```
1.     1
2.     11
3.     21
4.     1211
5.     111221
```

1 is read off as "one 1" or 11.<br>
11 is read off as "two 1s" or 21.<br>
21 is read off as "one 2, then one 1" or 1211.<br>

## Remarks

`i++`不能写在循环条件里，因为当前已经是待评估的了，如果不等于ch，还是会++一次，就会出错。要不就写成`i < s.Length-1 && s[++i] == ch`
