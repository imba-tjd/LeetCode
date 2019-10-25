// 二叉树
using System;
using System.Collections.Generic;
using Xunit;

namespace LCDS
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
        public static TreeNode Create(IEnumerable<int?> values, TraversalType ttype = TraversalType.Layer)
            => TreeNodeHelper.Dispatch<TreeNode>(nameof(Create), ttype, values);
    }

    public enum TraversalType
    {
        PreOrder, // DLR先序
        InOrder, // LDR中序
        PostOrder, // LRD后序
        Layer // 层次遍历
    }

    public static class TreeNodeHelper
    {
        internal static System.Reflection.MethodInfo GetFunction(string action, TraversalType ttype) =>
            typeof(TreeNodeHelper).GetMethod(action + "By" + ttype.ToString(),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        internal static T Dispatch<T>(string action, TraversalType ttype, params object[] args) =>
            (T)GetFunction(action, ttype).Invoke(null, args);
        static TreeNode CTN(int? val) => val == null ? null : new TreeNode(val.Value);

        internal static TreeNode CreateByLayer(IEnumerable<int?> values)
        {
            var e = values.GetEnumerator();
            if (e.MoveNext() == false)
                return null;

            var q = new Queue<TreeNode>();
            var root = CTN(e.Current);
            q.Enqueue(root);

            while (e.MoveNext() != false)
            {
                var c = q.Dequeue();

                if (c == null) // 为null的结点，左右孩子必须也为null
                    if (e.Current != null || e.MoveNext() && e.Current != null)
                        throw new ArrayTypeMismatchException("试图给不存在的结点添加孩子");
                    else
                        continue;

                c.left = CTN(e.Current);

                if (e.MoveNext() == false) // 允许最后为null的没有
                    break;
                c.right = CTN(e.Current);

                q.Enqueue(c.left); // 即使为null也要入队
                q.Enqueue(c.right);
            }

            return root;
        }
        internal static TreeNode CreateByPreOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();
        // {
        //     var e = values.GetEnumerator();
        //     if (e.MoveNext() == false)
        //         return null;

        //     var s = new Stack<TreeNode>();
        //     var root = CTN(e.Current);
        //     s.Push(root);

        //     while(e.MoveNext() != false)
        //     {
        //         var c = s.Pop();

        //     }

        //     return root;
        // }
        internal static TreeNode CreateByInOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();
        internal static TreeNode CreateByPostOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();

        public static IEnumerable<int?> AsEnumerable(this TreeNode tn, TraversalType ttype = TraversalType.PreOrder) =>
            Dispatch<IEnumerable<int?>>(nameof(AsEnumerable), ttype, tn);

        // 基本上就是书上的做法，只是为了返回null，循环条件改了一下
        internal static IEnumerable<int?> AsEnumerableByPreOrder(TreeNode tn)
        {
            var s = new Stack<TreeNode>();
            while (true) // 书上这里是s.Count!=0 || tn != null
            {
                yield return tn?.val; // 如果此处tn为null时不返回，就是纯的先序序列
                if (tn != null)
                {
                    s.Push(tn); // 压栈自己，把当前设为左边
                    tn = tn.left;
                }
                else
                {
                    if (s.Count == 0) // 当前为null**且**栈为空时结束
                        break;
                    tn = s.Pop().right; // 别忘了往右边走
                }
            }
        }

        // 只评估自己的做法。因为每次循环的当前来源是Pop出来的，必须立即消费，否则就留不下来了，因此只能用于先序遍历
        internal static IEnumerable<int?> AsEnumerableByPreOrder2(TreeNode tn)
        {
            var s = new Stack<TreeNode>();
            s.Push(tn);
            while (s.Count != 0)
            {
                tn = s.Pop();
                yield return tn?.val;
                if (tn != null)
                {
                    s.Push(tn.right);
                    s.Push(tn.left);
                }
            }
        }

        internal static IEnumerable<int?> AsEnumerableByInOrder(TreeNode tn)
        {
            var s = new Stack<TreeNode>();
            while (true)
            {
                if (tn != null)
                {
                    s.Push(tn);
                    tn = tn.left;
                }
                else
                {
                    yield return null;
                    if (s.Count == 0)
                        break;
                    tn = s.Pop();
                    yield return tn.val;
                    tn = tn.right;
                }
            }
        }

        internal static IEnumerable<int?> AsEnumerableByPostOrder(TreeNode tn)
            => throw new NotImplementedException();

        // 单个结点是否相等
        public static bool Equals(TreeNode p, TreeNode q) =>
            p == null && q == null ? true :
            p == null || q == null ? false :
            p.val == q.val;
    }

    public class TreeNodeTest
    {
        [Theory]
        [InlineData(TraversalType.Layer)]
        public void CreateTreeByLayerTest1(TraversalType ttype)
        {
            /*  3
               / \
              9  20
                /  \
               15   7 */
            var tn = TreeNode.Create(new int?[] { 3, 9, 20, null, null, 15, 7 }, ttype);
            // 按照先序断言
            Assert.Equal(3, tn.val);
            Assert.Equal(9, tn.left.val);
            Assert.Null(tn.left.left);
            Assert.Null(tn.left.right);
            Assert.Equal(20, tn.right.val);
            Assert.Equal(15, tn.right.left.val);
            Assert.Null(tn.right.left.left);
            Assert.Null(tn.right.left.right);
            Assert.Equal(7, tn.right.right.val);
            Assert.Null(tn.right.right.left);
            Assert.Null(tn.right.right.right);
        }
        [Theory]
        [InlineData(TraversalType.Layer)]
        public void CreateTreeByLayerTest2(TraversalType ttype)
        {
            /*     1
                  / \
                 2   2
                / \
               3   3
              / \
             4   4        */
            var tn = TreeNode.Create(new int?[] { 1, 2, 2, 3, 3, null, null, 4, 4 }, ttype);
            Assert.Equal(1, tn.val);
            Assert.Equal(2, tn.left.val);
            Assert.Equal(3, tn.left.left.val);
            Assert.Equal(4, tn.left.left.left.val);
            Assert.Null(tn.left.left.left.left);
            Assert.Null(tn.left.left.left.right);
            Assert.Equal(4, tn.left.left.right.val);
            Assert.Null(tn.left.left.right.left);
            Assert.Null(tn.left.left.right.right);
            Assert.Equal(3, tn.left.right.val);
            Assert.Null(tn.left.right.right);
            Assert.Null(tn.left.right.left);
            Assert.Equal(2, tn.right.val);
            Assert.Null(tn.right.left);
            Assert.Null(tn.right.right);
        }

        [Theory]
        [InlineData(TraversalType.PreOrder)]
        [InlineData(TraversalType.InOrder)]
        [InlineData(TraversalType.PostOrder)]
        [InlineData(TraversalType.Layer)]
        public void ReflectionTest(TraversalType ttype)
        {
            var f1 = TreeNodeHelper.GetFunction("Create", ttype);
            Func<IEnumerable<int?>, TreeNode> f2 = ttype switch
            {
                TraversalType.PreOrder => TreeNodeHelper.CreateByPreOrder,
                TraversalType.InOrder => TreeNodeHelper.CreateByInOrder,
                TraversalType.PostOrder => TreeNodeHelper.CreateByPostOrder,
                TraversalType.Layer => TreeNodeHelper.CreateByLayer,
                _ => throw new NotImplementedException()
            };
            Assert.Equal(f2.Method, f1);
        }

        [Fact]
        public static void AsEnumerableTest()
        {
            var tn = TreeNode.Create(new int?[] { 3, 9, 20, null, null, 15, 7 });
            var result = tn.AsEnumerable();
            var expect = new int?[] { 3, 9, null, null, 20, 15, null, null, 7, null, null };
            Assert.Equal(expect, result);
        }
    }
}
