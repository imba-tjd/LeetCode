# 3. Longest Substring Without Repeating Characters

Given a string, find the length of the longest substring without repeating characters.

## Remarks

需要一种手段记录以前读到的字符；且读到重复的字符时，slide window的左端应尽可能地右移，因为该重复字符不一定就在最左端。<br>
所以，拿int[]模拟dictionary，key为char，value为char的index。

我的思路：不重复时，dic存下已经读到的字符，直接继续读下一位。读到重复字符时，from才右移，清除中间的记录，更新长度；因为只有在重复时才更新from和maxlength，有可能完全没有重复过，所以最后还需要判断一次。

另一种思路：永远记录每个字符最后的位置，每次循环都更新from=Max(set[s[to]], from)以及maxlength。即如果那个字符的索引大于from就代表“存在”，避免了清除操作。细节上，实际保存的是那个字符之后的位置。但是速度比我的要慢。

本来如果记录不存在时我设置的是-1的，因为0是有效的索引；看了另一种思路后，改成了index+1，区别仅在清除时的条件，原先是(from < dic[key]+1)，而length不变。如果使用真正的dic就可以避免这种操作。
