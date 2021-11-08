using LeetCode.DataStructures;

namespace LeetCode.Problems.P112PathSum
{
    public interface ISolution { bool HasPathSum(TreeNode root, int sum); }
    class Solution : ISolution
    {
        public bool HasPathSum(TreeNode root, int sum) =>
            root == null ? false :
            HasPathSum(root.left, sum - root.val) ? true :
            HasPathSum(root.right, sum - root.val) ? true :
            root.left == null && root.right == null && root.val == sum;
        // 最后一句放到第二句效率稍微高一点，不会导致已经满足条件时还要左右递归两次返回false
    }

    // 与上面保持一致的解法，直接用逻辑运算符。注意&&和括号的变化
    class Solution2 : ISolution
    {
        public bool HasPathSum(TreeNode root, int sum) =>
            root != null && (
            HasPathSum(root.left, sum - root.val) ||
            HasPathSum(root.right, sum - root.val) ||
            root.val == sum && root.left == null && root.right == null);
    }

    class Solution3 : ISolution
    {
        public bool HasPathSum(TreeNode root, int sum)
        {
            var s = new Stack<(TreeNode, int)>();
            s.Push((root, sum));

            while (s.Count != 0)
            {
                (root, sum) = s.Pop();
                if (root != null)
                {
                    if (root.left == null && root.right == null && root.val == sum)
                        return true;
                    if (root.right != null)
                        s.Push((root.right, sum - root.val));
                    if (root.left != null)
                        s.Push((root.left, sum - root.val));
                }
            }

            return false;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 5, 4, 8, 11, null, 13, 4, 7, 2, null, null, null, 1 }, 22, true };
            yield return new object[] { new int?[] { 1, -2, -3, 1, 3, -2, null, -1 }, -1, true };
            yield return new object[] { new int?[] { -2, null, -3 }, -5, true };
            yield return new object[] { new int?[] { 1, 2 }, 1, false };
            yield return new object[] { new int?[] { }, 1, false };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] a, int sum, bool expect)
        {
            var so = GetSo;
            var result = so.HasPathSum(TreeNode.Create(a), sum);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { protected override ISolution GetSo => new Solution3(); }
}
