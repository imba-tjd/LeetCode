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
        public static TreeNode Create(IEnumerable<int?> values, TraversalType ttype = TraversalType.LayerByLayer)
            => (TreeNode)TreeNodeHelper.GetCreate(ttype).Invoke(null, new[] { values });
    }

    public enum TraversalType
    {
        PreOrder, // DLR先序
        InOrder, // LDR中序
        PostOrder, // LRD后序
        LayerByLayer // 层次遍历
    }

    public static class TreeNodeHelper
    {
        internal static System.Reflection.MethodInfo GetCreate(TraversalType ttype) =>
            typeof(TreeNodeHelper).GetMethod("Create" + ttype.ToString(),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        static TreeNode CTN(int? val) => val == null ? null : new TreeNode(val.Value);

        internal static TreeNode CreateLayerByLayer(IEnumerable<int?> values)
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
        internal static TreeNode CreatePreOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();
        internal static TreeNode CreateInOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();
        internal static TreeNode CreatePostOrder(IEnumerable<int?> values)
            => throw new NotImplementedException();

        // 单个结点是否相等
        public static bool Equals(TreeNode p, TreeNode q) =>
            p == null && q == null ? true :
            p != null && q != null ? p.val == q.val :
            false;
    }

    public class TreeNodeTest
    {
        [Theory]
        [InlineData(TraversalType.LayerByLayer)]
        public void CreateTreeLayerByLayerTest1(TraversalType ttype)
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
        [InlineData(TraversalType.LayerByLayer)]
        public void CreateTreeLayerByLayerTest2(TraversalType ttype)
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
        [InlineData(TraversalType.LayerByLayer)]
        public void ReflectionTest(TraversalType ttype)
        {
            var f1 = TreeNodeHelper.GetCreate(ttype);
            Func<IEnumerable<int?>, TreeNode> f2 = ttype switch
            {
                TraversalType.PreOrder => TreeNodeHelper.CreatePreOrder,
                TraversalType.InOrder => TreeNodeHelper.CreateInOrder,
                TraversalType.PostOrder => TreeNodeHelper.CreatePostOrder,
                TraversalType.LayerByLayer => TreeNodeHelper.CreateLayerByLayer,
                _ => throw new NotImplementedException()
            };
            Assert.Equal(f2.Method, f1);
        }
    }
}
