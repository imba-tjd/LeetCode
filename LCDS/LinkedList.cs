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
        // ListNode to int[]
        public static int[] ToArray(this ListNode ln) => ln.AsEnumerable().ToArray();

        // IEnumerable<int> to ListNode
        // 之前写在构造函数里，没法返回null，不能只管自己，复杂了许多。
        public static ListNode ToListNode(this IEnumerable<int> source)
            => source.Any() ? new ListNode(source.First()) { next = ToListNode(source.Skip(1)) } : null;

        // int to ListNode，每个数字一个结点，仅限正数。注意低位数字在前面的结点。
        public static ListNode ToListNode(this int value)
            => value == 0 ? null : new ListNode(value % 10) { next = (value / 10).ToListNode() };

        // 返回指定索引的结点，如果超出，返回null；index为负或ln为负时也返回null，这点与自带的不同，前者与ElementAtOrDefault一致。
        public static ListNode At(this ListNode ln, int index)
            => index == 0 ? ln : ln?.next?.At(index - 1);
        // {
        //     while (--index >= 0 && ln != null)
        //         ln = ln.next;
        //     return ln; // 已经包含了需要的或null
        // }

        // 有环的时候用不了，否则若要检测环就跟141和142题一样了
        public static ListNode Tail(this ListNode ln)
            => ln.next == null ? ln : Tail(ln.next);
        // {
        //     while (ln.next != null)
        //         ln = ln.next;
        //     return ln;
        // }
    }

    public class LinkedListTest
    {
        [Fact]
        void AsEnumerableAndToArrayAndToListNodeTest()
        {
            int[] arr = new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
            var ln = arr.ToListNode();
            var result = ln.ToArray();
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            for (int i = 0; i < arr.Length; i++)
                Assert.Equal(arr[i], result[i]);

            Assert.Null((new int[0]).ToListNode());
        }

        [Fact]
        void IntToListNodeAndAtAndTailTest()
        {
            ListNode ln = 123.ToListNode();
            Assert.Equal(3, ln.At(0).val);
            Assert.Equal(2, ln.At(1).val);
            Assert.Equal(1, ln.At(2).val);
            Assert.Null(ln.At(3));
            Assert.Null(ln.At(4));
            Assert.Equal(ln.At(2), ln.Tail());
        }
    }
}
