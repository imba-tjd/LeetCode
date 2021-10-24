using Xunit;
using LCDS;

namespace LeetCode.Problems.P141LinkedListCycle
{
    public interface ISolution { bool HasCycle(ListNode head); }
    class Solution : ISolution
    {
        public bool HasCycle(ListNode head)
        {
            HashSet<ListNode> hs = new HashSet<ListNode>();
            while (head != null)
            {
                if (hs.Contains(head))
                    return true;
                hs.Add(head);
                head = head.next;
            }
            return false;
        }
    }

    class Solution2 : ISolution
    {
        public bool HasCycle(ListNode head)
        {
            ListNode ln1, ln2;
            ln1 = head?.next;
            ln2 = head?.next?.next;
            while (ln2 != null)
            {
                if (ln1 == ln2)
                    return true;
                ln1 = ln1?.next;
                ln2 = ln2?.next?.next; // TODO: ln2永远不会是null
            }
            return false;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        [Theory]
        [InlineData(new[] { 3, 2, 0, -4 }, 1, true), InlineData(new[] { 1, 2 }, 0, true), InlineData(new[] { 1 }, -1, false)]
        public void Test(int[] arr, int pos, bool expect)
        {
            var so = GetSo;
            var ln = ListNode.Create(arr);
            if (pos != -1)
                ln.Tail().next = ln.At(pos);

            var result = so.HasCycle(ln);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
