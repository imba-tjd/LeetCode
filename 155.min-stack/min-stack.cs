using System;
using System.Collections.Generic;
using Xunit;

namespace MinStack
{
    class Cache
    {
        public Cache(int value) => (this.value, times) = (value, 1);
        public int value;
        public int times;
    }
    public class MinStack
    {
        Stack<int> stack = new Stack<int>();
        Stack<Cache> cache = new Stack<Cache>();
        public MinStack() => cache.Push(new Cache(int.MaxValue));
        public void Push(int x)
        {
            stack.Push(x);
            var min = cache.Peek();
            if (x == min.value)
                min.times++;
            else if (x < min.value)
                cache.Push(new Cache(x));
        }
        public void Pop()
        {
            int val = stack.Pop();
            var min = cache.Peek();
            if (val == min.value)
                if (min.times == 1)
                    cache.Pop();
                else
                    min.times--;
        }
        public int Top() => stack.Peek();
        public int GetMin() => cache.Peek().value;
    }

    public class UnitTest
    {
        [Fact]
        public void Test()
        {
            MinStack minStack = new MinStack();
            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            Assert.Equal(-3, minStack.GetMin());
            minStack.Pop();
            Assert.Equal(0, minStack.Top());
            Assert.Equal(-2, minStack.GetMin());
        }
    }
}
