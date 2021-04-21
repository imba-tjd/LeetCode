using System;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace LeetCode.Problems.P021MergeTwoSortedLists
{
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
            var l1 = ListNode.Create(l1a);
            var l2 = ListNode.Create(l2a);
            var so = GetSo;

            var result = so.MergeTwoLists(l1, l2).ToArray();
            Assert.Equal(expect, result);
        }

    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
