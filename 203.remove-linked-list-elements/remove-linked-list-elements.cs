using LeetCode.DataStructures;

namespace LeetCode.Problems.P203RemoveLinkedListElements
{
    public interface ISolution { ListNode RemoveElements(ListNode head, int val); }
    class Solution : ISolution
    {
        public ListNode RemoveElements(ListNode head, int val)
        {
            ListNode _head, current;
            _head = current = new ListNode(-1) { next = head }; // head是null也能工作

            while (current != null)
            {
                while (current.next != null && current.next.val == val)
                    current.next = current.next.next; // current是不变的
                current = current.next; // next是null接下来就出循环了，否则下一轮继续上面的比较
            }

            return _head.next;
        }
    }

    class Solution2 : ISolution
    {
        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
                return null;
            head.next = RemoveElements(head.next, val);
            return head.val == val ? head.next : head;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution();

        [Theory]
        [InlineData(new[] { 1, 2, 6, 3, 4, 5, 6 }, 6, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 1, 1, 1 }, 1, new int[] { })]
        public void Test(int[] input, int val, int[] expect)
        {
            var ln = ListNodeHelper.Create(input);
            var result = So.RemoveElements(ln, val);
            Assert.Equal(expect, ListNodeHelper.AsEnumerable(result));
        }
    }
    public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
