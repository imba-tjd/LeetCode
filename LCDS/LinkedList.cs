using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace LCDS
{
    // Definition for singly-linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
    public static class ListNodeHelper
    {
        // ListNode to IEnumerable<int>
        public static IEnumerable<int> AsEnumerable(this ListNode ln)
        {
            yield return ln.val;
            if (ln.next != null)
                foreach (var e in ln.next.AsEnumerable())
                    yield return e;
        }

        // IEnumerable<int> to ListNode
        // 之前写的另一种实现：
        // (val, next) = (source.First(), source.Skip(1).Any() ? new ListNode(source.Skip(1)) : null);
        public static ListNode ToListNode(this IEnumerable<int> source)
        {
            var ln = new ListNode(source.First());
            var rest = source.Skip(1);
            if (rest.Any())
                ln.next = ToListNode(rest);
            return ln;
        }

        // int to ListNode，每个数字一个结点，仅限正数。
        public static ListNode ToListNode(this int value)
            => value == 0 ? null : new ListNode(value % 10) { next = (value / 10).ToListNode() };

    }

    public class LinkedListTest
    {
        [Theory]
        [InlineData(new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 })]
        void AsEnumerableTest(int[] arr)
        {
            var ln = arr.ToListNode();
            var result = ln.AsEnumerable().ToArray();
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            for (int i = 0; i < arr.Length; i++)
                Assert.Equal(arr[i], result[i]);
        }

        [Fact]
        void IntToListNodeTest()
        {
            ListNode ln = 123.ToListNode();
            Assert.Equal(3, ln.val);
            Assert.Equal(2, ln.next.val);
            Assert.Equal(1, ln.next.next.val);
            Assert.Null(ln.next.next.next);
        }
    }
}
