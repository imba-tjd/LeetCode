# [28. Implement strStr()](https://leetcode.com/problems/implement-strstr/)

Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

当needle为空时选择返回0，这与C的strstr和Java的indexOf一致。

## Remarks

BF算法，因为需要内层循环完成后才能判断是否成功，第一想法是加个bool变量。后来看了别人的解析发现可以用j的位置判断。

## KMP算法

让text的指针i不回溯，仅调整pattern的指针j。

假设在j处不匹配（此时i==j），则`text[i-j..i-1]` == `pattern[0..j-1]`。若此时匹配完的text的后段存在与pattern的前段相等的串，那一段就可以不用重复匹配。

典型场景是以abcabc为pattern匹配abcabd。

复杂度O(m+n)

### PMT部分匹配值数组

对pattern的所有前缀，依次求它们的前缀真子串集合与后缀真子串集合的交集中最长的值。注意后缀也是从前往后的，只不过长度是从后往前增长。

如对于ababa：
  1. a的前缀和后缀是空集，0
  2. ab前缀为a，后缀为b，0
  3. aba前缀为{a,ab}，后缀为{a,ba}，交集中最长的为a，1
  4. abab前缀为{a,ab,aba}，后缀为{b,ab,bab}，交集中最长的为ab，2
  5. ababa前缀为{a,ab,aba,abab}，后缀为{a,ba,aba,baba}，交集中最长的为aba，3
因此它的PMT是00123。

当遇到不匹配时，将pattern往后移 已匹配的字符数 - 对应的PM值（即当前位置的前一个对应的）。若PM值为0，则移动的距离最大。

但直接使用它不方便。

### next数组

1. 看上去是PMT数组右移后，`next[0]=-1`形成的。
2. 实际上是p与p往右移一位后的两个串用KMP算法的思想：
   1. 匹配成功时`next[j+1] == next[j]+1`，往后移。
   2. 失败时j不动，`k=next[k]`再进行下一轮循环，直到`k==-1`或匹配成功。
3. 好像若索引从1开始，还要再对每一项+1。

### KMP算法

1. 循环结束时：
   1. 如果`j!=p.Length`，即匹配失败。
   2. 否则匹配成功，`j==p.Length`，i也位于对应字符串的后一个位置，所以结果为`i-j`。
   3. 因为i和j一定需要在循环外使用，所以不适合用for。
2. 循环中：
   1. 如果当前字符匹配，两个指针都++。
   2. 否则匹配失败，**i不动**，仍指向待评估的位置，`j=next[j]`，也指向待评估的位置；所以不适合用for。
   3. 2的特殊情况：当`j==0`时，如果还匹配失败，理论上应该j保持0不变，i++；实际上如果`next[0]==-1`，再在1中加`j==-1`的进入条件，i能++，j由-1加一刚好变为0。

### nextval

模式aaaab在和主串aaabaaaaab进行匹配时，如果用前面的next数组，还会进行几次无意义的匹配。不应出现p[j]==p[next[j]]，如果出现了，则要递归将next[j]修正为next[next[j]]。得到的就是nextval。

### 我自己尝试的实现：

1. 试图每次循环都把事情做完，一定进行i++；实际上代码中有个i--，这样与原来手动i++也没区别了。
2. 因为最后要用到i和j，所以它们的声明也不能只在for里面；最后一个return是错的，过不了`"aab", "ab"`。
3. GetNext中，试图以DP的方式思维，匹配成功的部分是没问题的，但是失败时并不能直接设为0，因为还可能有较短的部分匹配，此时感觉DP用不了了，因为`next[j]`也可能匹配失败，需要循环直到等于-1或匹配成功。

### 参考

* https://www.zhihu.com/question/21923021 但最高赞的代码有bug，评论区里指出了
* https://www.cnblogs.com/yjiyjige/p/3263858.html
