using Xunit;
using LCDS;

namespace LeetCode.Problems.P002AddTwoNumbers
{
    class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2) => AddInternal(l1, l2, 0);
        ListNode AddInternal(ListNode l1, ListNode l2, int carry)
        {
            int t = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
            return l1 == null && l2 == null && carry == 0 ? null :
                new ListNode(t % 10) { next = AddInternal(l1?.next, l2?.next, t / 10) };
        }
    }

    public class UnitTest
    {
        [Theory]
        [InlineData(1, 2, new[] { 3 })]
        [InlineData(342, 465, new[] { 7, 0, 8 })]
        [InlineData(3, 9, new[] { 2, 1 })]
        void Test(int a, int b, int[] expects)
        {
            ListNode l1 = ListNode.Create(a);
            ListNode l2 = ListNode.Create(b);
            var solution = new Solution();

            ListNode r = solution.AddTwoNumbers(l1, l2);
            foreach (int n in expects)
            {
                Assert.Equal(n, r.val);
                r = r.next;
            }
            Assert.Null(r);
        }
    }
}
