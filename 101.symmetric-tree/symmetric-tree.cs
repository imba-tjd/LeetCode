using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace Problems.Problem101.SymmetricTree
{
    public interface ISolution { bool IsSymmetric(TreeNode root); }

    class Solution : ISolution
    {
        public bool IsSymmetric(TreeNode root) => IsSymmetric(root.left, root.right);
        bool IsSymmetric(TreeNode p, TreeNode q) =>
            p == null && q == null ? true :
            p == null || q == null ? false :
            p.val == q.val && IsSymmetric(p.left, q.right) && IsSymmetric(p.right, q.left);
    }

    class Solution2 : ISolution
    {
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
                return true;

            Stack<TreeNode> s; // 其实用队列更好，那是广度优先
            s = new Stack<TreeNode>();
            s.Push(root.left);
            s.Push(root.right);

            while (s.Count != 0)
            {
                var a = s.Pop();
                var b = s.Pop();

                if (a == null && b == null)
                    continue;
                else if (a == null || b == null)
                    return false; // 必定是一个为null一个不为null
                else if (a.val != b.val)
                    return false;
                else
                {
                    s.Push(a.left);
                    s.Push(b.right); // 用一个就注意顺序
                    s.Push(a.right);
                    s.Push(b.left);
                }
            }

            return true;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 1, 2, 2, 3, 4, 4, 3 }, true };
            yield return new object[] { new int?[] { 1, 2, 2, null, 3, null, 3 }, false };
            yield return new object[] { new int?[] { 1, 2, 3 }, false };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, bool expect)
        {
            var so = GetSo;
            var result = so.IsSymmetric(TreeNode.Create(treearr));
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
