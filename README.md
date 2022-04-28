# [LeetCode](https://leetcode.com/problemset/all)

[![CI Status](https://github.com/imba-tjd/LeetCode/actions/workflows/ci.yml/badge.svg)](https://github.com/imba-tjd/LeetCode/actions)

记录一下我自己的刷题代码。\
LeetCode for myself.

大部分用的C#，用xUnit做单元测试，配了CI。\
Using mostly C#, tested with xUnit and CI.

代码基本结构用template文件夹中的程序生成。\
The program in template folder generates basic code.

希望能坚持吧。\
Hope I can stick to it.

## xUnit使用方法

当需要测试某一实现时，注释掉`abstract`，修改`So`返回指定解法；这样可以在VSC中inline debug。当需要进行多重测试时，取消注释abstract，继承出多个普通类重写So；适用于`dotnet test`。
