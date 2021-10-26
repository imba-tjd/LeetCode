using Xunit;
using LeetCode.DataStructures;

namespace LeetCode.Problems.P110BalancedBinaryTree
{
    public interface ISolution { bool IsBalanced(TreeNode root); }
    class Solution : ISolution
    {
        public bool IsBalanced(TreeNode root) => GetDepth(root) != -1; // 使用-1来判断是否平衡，不用另外的变量

        int GetDepth(TreeNode tn) =>
            tn == null ? 0 :
            GetDepth(tn.left) is int l && l == -1 ? -1 :
            GetDepth(tn.right) is int r && r == -1 ? -1 :
            Math.Abs(l - r) > 1 ? -1 :
            Math.Max(l, r) + 1;
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 3, 9, 20, null, null, 15, 7 }, true };
            yield return new object[] { new int?[] { 1, 2, 2, 3, 3, null, null, 4, 4 }, false };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, bool expect)
        {
            var so = GetSo;
            var tree = TreeNode.Create(treearr);
            var result = so.IsBalanced(tree);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
