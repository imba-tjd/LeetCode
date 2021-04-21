using System;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace Problems.Problem226.InvertBinaryTree
{
    public interface ISolution { TreeNode InvertTree(TreeNode root); }
    class Solution : ISolution
    {
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
                return null;

            // (root.left, root.right) = (root.right, root.left);
            // InvertTree(root.left);
            // InvertTree(root.right);
            // 或者：（不能分开写，必须要有元组，要不就再创建一个temp）
            (root.left, root.right) = (InvertTree(root.right), InvertTree(root.left));

            return root;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 4, 2, 7, 1, 3, 6, 9 }, new int?[] { 4, 7, 2, 9, 6, 3, 1 } };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, int?[] expect)
        {
            var so = GetSo;
            var tree = TreeNode.Create(treearr);
            var result = so.InvertTree(tree);
            Assert.Equal(expect, result.AsEnumerable());
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
