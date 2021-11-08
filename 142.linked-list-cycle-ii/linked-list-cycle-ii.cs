using LeetCode.DataStructures;

namespace LeetCode.Problems.P142LinkedListCycleII
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
            ln1 = ln2 = head;

            do
            {
                ln1 = ln1?.next;
                ln2 = ln2?.next?.next;
                if (ln2 == null)
                    return null;
            } while (ln1 != ln2);

            ListNode ln3 = head;
            while (ln3 != ln1)
                (ln3, ln1) = (ln3.next, ln1.next);

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
            var ln = ListNodeHelper.Create(arr);
            if (pos != -1) // 我的At里是如果index不存在则返回null，但不能依赖它，因为正确效果是抛异常
                ListNodeHelper.Tail(ln).next = ListNodeHelper.At(ln, pos);

            var result = so.DetectCycle(ln);
            Assert.Equal(pos == -1 ? null : ListNodeHelper.At(ln, pos), result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
