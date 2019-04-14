# [155. Min Stack](https://leetcode.com/problems/min-stack/)

 Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.

* push(x) -- Push element x onto stack.
* pop() -- Removes the element on top of the stack.
* top() -- Get the top element.
* getMin() -- Retrieve the minimum element in the stack.

## Remarks

Know from <https://www.youtube.com/watch?v=nGwn8_-6e7w>.

本来以为是普通的栈，记录一下min就好。Pop后傻眼了，最小值只能遍历找。后来想到同时使用栈和SortedArray，但这样Push和Pop就不是O(1)了。

其实关键是每次Push都需要记录当前的最小值（State）。一种方式自然是每次都直接把当前值和最小值Push进去，但遇到`[0,1,1,1,1]`这种就会空耗许多空间。

[一种改进方法](https://leetcode.com/problems/min-stack/discuss/269091/C%2B%2B-one-stack)是，还是单独记录min，当Push的值小于等于min时，先Push min，再Push值；而Pop时如果出来的值等于min，就再Pop一次，把这次作为新的min。但遇到`[0,0,0,0,1]`这种还是会多次记录最小值。

最好的方式就是另开一个栈(Cache)记录最小值，如果Pop出的是最小值就把Cache也给Pop掉，**否则不变**；Cache的栈顶就永远直接是最小值。实际上因为同一个值可以出现多次，连续出现两次相同的最小值时就不能直接Pop Cache，而是要记录次数，次数为0时才去掉。但这样需要用到引用类型，否则用元组就只能先Pop再Push，无法直接修改次数。这种方法如果手动使用数组模拟栈，速度没有任何差别。

依次对第二种方法再改进：单独记录times，当x<min时把times也push进去。不过结果上速度反而慢了4ms。
