using System;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace Problems.Problem100.SameTree
{
    public interface ISolution { bool IsSameTree(TreeNode p, TreeNode q); }
    class Solution : ISolution
    {
        public bool IsSameTree(TreeNode p, TreeNode q) =>
            p == null && q == null ? true :
            p == null || q == null ? false : // 有且只有一个为null
            p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        // 都不为null。本来是把有一个为null的放在最后的else里的，后来发现那样第二条太长了，第三条就一个false
    }

    // 和第一种方法一样，只是换成了迭代。另外也可以用一个队列解决。
    class Solution2 : ISolution
    {
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            Queue<TreeNode> pq, qq;
            pq = new Queue<TreeNode>();
            qq = new Queue<TreeNode>();
            pq.Enqueue(p);
            qq.Enqueue(q);

            while (pq.Count != 0 && qq.Count != 0)
            {
                var a = pq.Dequeue();
                var b = qq.Dequeue();
                if (a == null && b == null) // 都为null时不也没法把孩子如队
                    continue;
                else if (a == null || b == null)
                    return false;
                else if (a.val != b.val)
                    return false;
                else
                {
                    pq.Enqueue(a.left);
                    pq.Enqueue(a.right);
                    qq.Enqueue(b.left);
                    qq.Enqueue(b.right);
                }
            }

            return true; // 可以保证两者是同步的，也只有这样才能只用一个队列
            // return pq.Count == 0 && qq.Count == 0;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 1, 2, 3 }, new int?[] { 1, 2, 3 }, true };
            yield return new object[] { new int?[] { 1, 2 }, new int?[] { 1, null, 2 }, false };
            yield return new object[] { new int?[] { 1, 2, 1 }, new int?[] { 1, 1, 2 }, false };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] a, int?[] b, bool expect)
        {
            var so = GetSo;
            var result = so.IsSameTree(TreeNode.Create(a), TreeNode.Create(b));
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
}
