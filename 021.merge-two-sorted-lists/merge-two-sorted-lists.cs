using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace MergeTwoSortedLists
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
        public ListNode(IEnumerable<int> source) =>
            (val, next) = (source.First(), source.Skip(1).Any() ? new ListNode(source.Skip(1)) : null);
        public IEnumerable<int> AsEnumerable()
        {
            yield return val;
            if (next != null)
                foreach (var e in next.AsEnumerable())
                    yield return e;
        }
    }
    public interface ISolution { ListNode MergeTwoLists(ListNode l1, ListNode l2); }
    class Solution : ISolution
    {
        readonly ListNode head = new ListNode(0);
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var current = head;
            while (l1 != null && l2 != null)
                if (l1.val < l2.val)
                {
                    current = current.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    current = current.next = l2;
                    l2 = l2.next;
                }
            current.next = l1 ?? l2;
            return head.next;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        [Theory]
        [InlineData(new[] { 1, 2, 4 }, new[] { 1, 3, 4 }, new[] { 1, 1, 2, 3, 4, 4 })]
        public void Test(int[] l1a, int[] l2a, int[] expect)
        {
            var l1 = new ListNode(l1a);
            var l2 = new ListNode(l2a);
            // var expect = new ListNode(expecta);
            var so = GetSo;
            var result = so.MergeTwoLists(l1, l2).AsEnumerable().ToArray();

            for (int i = 0; i < expect.Length; i++)
                Assert.Equal(expect[i], result[i]);
        }

    }
    // public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }

    public class AsEnumerableTest
    {
        [Theory]
        [InlineData(new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 })]
        public void Test(int[] arr)
        {
            var list = new ListNode(arr);
            var result = list.AsEnumerable().ToArray();
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            for (int i = 0; i < arr.Length; i++)
                Assert.Equal(arr[i], result[i]);
        }
    }
}
