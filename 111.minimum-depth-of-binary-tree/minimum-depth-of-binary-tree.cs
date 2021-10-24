using Xunit;
using LCDS;

namespace LeetCode.Problems.P111MinimumDepthofBinaryTree
{
    public interface ISolution { int MinDepth(TreeNode root); }
    class Solution : ISolution
    {
        public int MinDepth(TreeNode root) =>
            root == null ? 0 :
            root.left != null && root.right != null ? Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1 :
            MinDepth(root.left ?? root.right) + 1; // 左右孩子中有一个为null（或都为null，下一次递归返回0）
    }

    // 层次遍历计算最小叶子深度
    class Solution2 : ISolution
    {
        public int MinDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            int depth = 0;
            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                depth++;
                int count = q.Count; // 要单独写一下，否则每次循环会重新评估数量
                for (int i = 0; i < count; i++)
                {
                    var c = q.Dequeue();
                    if (c.left == null && c.right == null)
                        return depth;

                    if (c.left != null)
                        q.Enqueue(c.left);
                    if (c.right != null)
                        q.Enqueue(c.right);
                }
            }

            return depth;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution2();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 3, 9, 20, null, null, 15, 7 }, 2 };
            yield return new object[] { new int?[] { 1 }, 1 };
            yield return new object[] { new int?[] { }, 0 };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, int expect)
        {
            var so = GetSo;
            var tree = TreeNode.Create(treearr);
            var result = so.MinDepth(tree);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
