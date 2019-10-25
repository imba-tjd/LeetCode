using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using LCDS;

namespace MaximumDepthofBinaryTree
{
    public interface ISolution { int MaxDepth(TreeNode root); }
    class Solution : ISolution
    {
        public int MaxDepth(TreeNode root) =>
            root == null ? 0 :
            Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
    }

    // 非递归先序遍历，但出栈时不能currentdepth--，因为右子树出栈时自己已经不在栈里了。所以需要保存当前深度
    class Solution2 : ISolution
    {
        public int MaxDepth(TreeNode root)
        {
            var s = new Stack<(TreeNode, int)>();
            int maxdepth = 0, currentdepth = 0;

            while (s.Count != 0 || root != null)
            {
                if (root != null)
                {
                    if (++currentdepth > maxdepth)
                        maxdepth = currentdepth;
                    s.Push((root, currentdepth));
                    root = root.left;
                }
                else
                {
                    (root, currentdepth) = s.Pop();
                    root = root.right;
                }
            }

            return maxdepth;
        }
    }

    // 非递归层次遍历。思路是遇到每一层的最后一个时（current==last）层数加一，此时队列里刚好全部是下一层的结点，则下一层的最后一个刚好是队尾。
    // 需要获取队尾，原生不支持，用了Linq导致效率不高。因为null永远等于null，所以队列里不能有null
    // 也可以不用last，而是在里面用个循环，因为每一层开始时队列的长度是知道的，只要把当时大小的队列遍历一遍就行。
    class Solution3 : ISolution
    {
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            int depth = 0;
            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            var last = root;
            while (q.Count != 0)
            {
                var c = q.Dequeue();
                if (c.left != null)
                    q.Enqueue(c.left);
                if (c.right != null)
                    q.Enqueue(c.right);

                if (c == last)
                {
                    depth++;
                    last = q.LastOrDefault();
                }
            }

            return depth;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 3, 9, 20, null, null, 15, 7 }, 3 };
            yield return new object[] { new int?[] { 1, null, null }, 1 };
            yield return new object[] { new int?[] { null }, 0 };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, int expect)
        {
            var so = GetSo;
            var tree = TreeNode.Create(treearr);
            var result = so.MaxDepth(tree);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { override protected ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { override protected ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { override protected ISolution GetSo => new Solution3(); }
}
