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

        // 不存在头节点，这样删除的时候也没办法删除第一个元素，203题是返回新的头结点
        public static ListNode Create(IEnumerable<int> source) =>
            source.Any() ? new ListNode(source.First()) { next = Create(source.Skip(1)) } : null;
        // 说明见下面注释掉的ToListNode函数
        public static ListNode Create(int value) =>
            value == 0 ? null : new ListNode(value % 10) { next = ListNode.Create(value / 10) };
    }
    public static class ListNodeHelper
    {
        // ListNode to IEnumerable<int>
        public static IEnumerable<int> AsEnumerable(this ListNode ln)
        {
            if (ln == null) // 如果LN是null，不会产生NPE，而是返回空序列
                yield break; // 在非yield函数中就是Enumerable.Empty
            yield return ln.val;
            foreach (var e in ln.next.AsEnumerable()) // 不必检查ln.next!=null，原因见上面那句注释
                yield return e;
        }
        // ListNode to int[]
        public static int[] ToArray(this ListNode ln) => ln.AsEnumerable().ToArray();

        // IEnumerable<int> to ListNode
        // 之前写在构造函数里，没法返回null，不能只管自己，复杂了许多。
        // 现在改成ListNode类的静态函数，不作为扩展函数
        // public static ListNode ToListNode(this IEnumerable<int> source) =>
        //     source.Any() ? new ListNode(source.First()) { next = ToListNode(source.Skip(1)) } : null;

        // int to ListNode，每个数字一个结点，仅限正数。注意低位数字储存在前面的结点（逆序）。
        // 同上
        // public static ListNode ToListNode(this int value) =>
        //     value == 0 ? null : new ListNode(value % 10) { next = (value / 10).ToListNode() };

        // 返回指定索引的结点，如果超出，返回null；index为负或ln为负时也返回null，这点与自带的不同，前者与ElementAtOrDefault一致。
        public static ListNode At(this ListNode ln, int index) =>
            index == 0 ? ln : ln?.next?.At(index - 1);
        // {
        //     while (--index >= 0 && ln != null)
        //         ln = ln.next;
        //     return ln; // 已经包含了需要的或null
        // }

        // 有环的时候用不了，否则若要检测环就跟141和142题一样了
        public static ListNode Tail(this ListNode ln) =>
            ln.next == null ? ln : Tail(ln.next);
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
            var ln = ListNode.Create(arr);
            var result = ln.ToArray();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(arr, result);

            var none = ListNode.Create(new int[0]);
            Assert.Null(none);
            Assert.Empty(none.AsEnumerable());
        }

        [Fact]
        void IntToListNodeAndAtAndTailTest()
        {
            ListNode ln = ListNode.Create(123);
            Assert.Equal(3, ln.At(0).val);
            Assert.Equal(2, ln.At(1).val);
            Assert.Equal(1, ln.At(2).val);
            Assert.Null(ln.At(3));
            Assert.Null(ln.At(4));
            Assert.Equal(ln.At(2), ln.Tail());
        }
    }
}
