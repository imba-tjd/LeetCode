# nullable enable

using Xunit;
using System.Linq;

namespace LeetCode.DataStructures
{
    public class ListNode<T>
    {
        public T val;
        public ListNode<T>? next;
        public ListNode(T v, ListNode<T>? nxt = null) => (val, next) = (v, nxt);
    }

    public class ListNode : ListNode<int>
    {
        ListNode? _next;
        public new ListNode? next { get => _next; set { base.next = value; _next = value; } }
        public ListNode(int v, ListNode? nxt = null) : base(v, nxt) => next = nxt;
    }

    public static class ListNodeHelper
    {
        // 从父类转换成子类，需要全部创建新的结点
        public static ListNode? Create(ListNode<int>? ln) =>
            ln is null ? null : new ListNode(ln.val, Create(ln.next));

        // 序列转换成链表。这样创建不存在头结点，也没办法删除第一个元素；203题是返回新的头结点
        public static ListNode<T>? Create<T>(IEnumerable<T> src) =>
            !src.Any() ? null : new ListNode<T>(src.First(), Create(src.Skip(1)));
        public static ListNode? Create(IEnumerable<int> src) => Create(Create<int>(src));

        // 把一个正整数值按每一位转换成链表，注意低位数字储存在前面的结点（逆序）。0当作不存在
        public static ListNode? Create(int value) =>
            value == 0 ? null : new(value % 10, Create(value / 10));

        // 链表转序列
        public static IEnumerable<T> AsEnumerable<T>(ListNode<T>? ln)
        {
            if (ln is null) // 如果列表是null，不产生NPE，而是返回空序列
                yield break;
            yield return ln.val;
            foreach (var e in AsEnumerable(ln.next)) // 不必考虑next是不是null，递归中处理了
                yield return e;
        }

        // 取指定索引的结点。如果超出，返回null；不考虑index为负数的情形
        public static ListNode<T>? At<T>(ListNode<T>? ln, int index) =>
            index == 0 || ln is null ? ln : At(ln.next, index - 1);
        public static ListNode? At(ListNode? ln, int index) => At<int>(ln, index) as ListNode;

        // 取最后一个结点。不考虑存在环
        public static ListNode<T> Tail<T>(ListNode<T> ln) =>
            ln.next == null ? ln : Tail(ln.next);
        public static ListNode Tail(ListNode ln) => (ListNode)Tail<int>(ln);
    }
}

namespace LeetCode.DataStructures.Test
{
    public class LinkedListTest
    {
        [Fact]
        void AsEnumerableAndToArrayAndToListNodeTest()
        {
            int[] arr = new[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
            var ln = ListNodeHelper.Create(arr);
            var result = ListNodeHelper.AsEnumerable(ln).ToArray();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(arr, result);

            var none = ListNodeHelper.Create(new int[0]);
            Assert.Null(none);
            Assert.Empty(ListNodeHelper.AsEnumerable(none));
        }

        [Fact]
        void IntToListNodeAndAtAndTailTest()
        {
            ListNode ln = ListNodeHelper.Create(123)!;
            Assert.Equal(3, ListNodeHelper.At(ln, 0)!.val);
            Assert.Equal(2, ListNodeHelper.At(ln, 1)!.val);
            Assert.Equal(1, ListNodeHelper.At(ln, 2)!.val);
            Assert.Null(ListNodeHelper.At(ln, 3));
            Assert.Null(ListNodeHelper.At(ln, 4));
            Assert.Equal(ListNodeHelper.At(ln, 2), ListNodeHelper.Tail(ln));
        }
    }
}
