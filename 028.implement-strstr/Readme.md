# [28. Implement strStr()](https://leetcode.com/problems/implement-strstr/)

Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

当needle为空时选择返回0，这与C的strstr和Java的indexOf一致。

## Remarks

BF算法，因为需要内层循环完成后才能判断是否成功，第一想法是加个bool变量。后来看了别人的解析发现可以用j的位置判断。

## KMP算法

1. 让text的指针i不回溯，仅调整pattern的指针j。
2. 假设在j处不匹配，则`text[i-j..i-1]` == `pattern[0..j-1]`。
3. 当匹配完的text的后段与pattern的前段相等时，那一段就可以不用重复匹配了；根据2其实就是pattern的前段和后端有完全相等的串。
4. 注意前后端的串都是从前往后的，比如abcabd，不是abccba这样的。

PMT数组：

1. 对pattern的所有前缀子串，求它们的前缀子串集合与后缀子串集合的交集中最长的值。
2. 前后缀子串都不包含pattern自己，即真子串。

KMP算法：

1. 循环结束时：
   1. 如果`j!=p.Length`，即匹配失败。
   2. 否则匹配成功，`j==p.Length`，i也位于对应字符串的后一个位置，所以结果为`i-j`。
   3. 因为i和j一定需要在循环外使用，所以不适合用for。
2. 循环中：
   1. 如果当前字符匹配，两个指针都++。
   2. 否则匹配失败，**i不动**，仍指向待评估的位置，`j=next[j]`，也指向待评估的位置；所以不适合用for。
   3. 2的特殊情况：当`j==0`时，如果还匹配失败，理论上应该j保持0不变，i++；实际上如果`next[0]==-1`，再在1中加`j==-1`的进入条件，i能++，j由-1加一刚好变为0。

next数组：

1. 看上去是PMT数组右移后，`next[0]=-1`形成的。
2. 实际上是p与p往右移一位后的两个串用KMP算法的思想：
   1. 匹配成功时`next[j+1] == next[j]+1`，往后移。
   2. 失败时j不动，`k=next[k]`再进行下一轮循环，直到`k==-1`或匹配成功。

我自己尝试的实现：

1. 试图每次循环都把事情做完，一定进行i++；实际上代码中有个i--，这样与原来手动i++也没区别了。
2. 因为最后要用到i和j，所以它们的声明也不能只在for里面；最后一个return是错的，过不了`"aab", "ab"`。
3. GetNext中，试图以DP的方式思维，匹配成功的部分是没问题的，但是失败时并不能直接设为0，因为还可能有较短的部分匹配，此时感觉DP用不了了，因为`next[j]`也可能匹配失败，需要循环直到等于-1或匹配成功。

参考：

* https://www.zhihu.com/question/21923021 但最高赞的代码有bug，评论区里指出了
* https://www.cnblogs.com/yjiyjige/p/3263858.html
