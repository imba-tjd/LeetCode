using System;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace Problems.LinkedListCycleII
{
    public interface ISolution { ListNode DetectCycle(ListNode head); }
    class Solution : ISolution
    {
        public ListNode DetectCycle(ListNode head)
        {
            HashSet<ListNode> hs = new HashSet<ListNode>();
            while (head != null)
            {
                if (hs.Contains(head))
                    return head;
                hs.Add(head);
                head = head.next;
            }
            return null;
        }
    }

    class Solution2 : ISolution
    {
        public ListNode DetectCycle(ListNode head)
        {
            ListNode ln1, ln2;
            ln1 = head?.next;
            ln2 = head?.next?.next;
            while (ln2 != null)
            {
                if (ln1 == ln2)
                    goto outer;
                ln1 = ln1?.next;
                ln2 = ln2?.next?.next;
            }
            return null;

        outer: // 一定有环
            ListNode ln3 = head;
            while (ln3 != ln1)
            {
                ln3 = ln3.next;
                ln1 = ln1.next;
            }
            return ln1;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { 3, 2, 0, -4 }, 1), InlineData(new[] { 1, 2 }, 0), InlineData(new[] { 1 }, -1)]
        public void Test(int[] arr, int pos)
        {
            var so = GetSo;
            var ln = ListNode.Create(arr);
            if (pos != -1) // 我的At里是如果index不存在则返回null，但不能依赖它，因为正确效果是抛异常
                ln.Tail().next = ln.At(pos);

            var result = so.DetectCycle(ln);
            Assert.Equal(pos == -1 ? null : ln.At(pos), result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
