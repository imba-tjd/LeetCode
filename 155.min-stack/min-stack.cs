
namespace LeetCode.Problems.P155MinStack
{
    public interface ISolution
    {
        void Push(int x);
        void Pop();
        int Top();
        int GetMin();
    }

    public class Solution : ISolution
    {
        class Cache
        {
            public Cache(int value) => (this.value, times) = (value, 1);
            public int value;
            public int times;
        }
        Stack<int> stack = new Stack<int>();
        Stack<Cache> cache = new Stack<Cache>();
        public Solution() => cache.Push(new Cache(int.MaxValue));
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

    class Solution2 : ISolution
    {
        int min = int.MaxValue;
        int times = 1;
        Stack<int> stack = new Stack<int>();
        public void Push(int x)
        {
            if (x == min)
                times++;
            else if (x < min)
            {
                stack.Push(min);
                stack.Push(times);
                min = x;
                times = 1;
            }
            stack.Push(x);
        }
        public void Pop()
        {
            int val = stack.Pop();
            if (val == min)
                if (times != 1)
                    times--;
                else
                {
                    times = stack.Pop();
                    min = stack.Pop();
                }
        }
        public int Top() => stack.Peek();
        public int GetMin() => min;
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Fact]
        public void Test()
        {
            var minStack = GetSo;

            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            Assert.Equal(-3, minStack.GetMin());
            minStack.Pop();
            Assert.Equal(0, minStack.Top());
            Assert.Equal(-2, minStack.GetMin());
        }
        [Fact]
        public void Test2()
        {
            var minStack = GetSo;

            minStack.Push(0);
            minStack.Push(1);
            minStack.Push(0);
            Assert.Equal(0, minStack.GetMin());
            minStack.Pop();
            Assert.Equal(0, minStack.GetMin());
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
