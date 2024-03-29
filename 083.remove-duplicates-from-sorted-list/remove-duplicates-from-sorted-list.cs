using LeetCode.DataStructures;
using System.Linq;

namespace LeetCode.Problems.P083RemoveDuplicatesfromSortedList
{
    public interface ISolution { ListNode DeleteDuplicates(ListNode head); }
    class Solution : ISolution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
                return null;

            ListNode _head = head;

            while (head.next != null)
                if (head.next.val == head.val)
                    head.next = head.next.next;
                else
                    head = head.next;

            return _head;
        }
    }

    class Solution2 : ISolution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
                return null;

            ListNode _head = head;
            ListNode pre = head, current = head.next;
            while (current != null)
            {
                if (current.val != pre.val)
                {
                    pre.next = current;
                    pre = current;
                }
                current = current.next;
            }
            pre.next = null;
            return _head;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution2();

        [Theory]
        [InlineData(new[] { 1, 1, 2 }, new[] { 1, 2 }), InlineData(new[] { 1, 1, 2, 3, 3 }, new[] { 1, 2, 3 })]
        public void Test(int[] input, int[] expect)
        {
            var result = ListNodeHelper.AsEnumerable(So.DeleteDuplicates(ListNodeHelper.Create(input))).ToArray();
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
