using Xunit;
using LCDS;

namespace LeetCode.Problems.P107BinaryTreeLevelOrderTraversalII
{
    public interface ISolution { IList<IList<int>> LevelOrderBottom(TreeNode root); }
    class Solution : ISolution
    {
        public IList<IList<int>> LevelOrderBottom(TreeNode root) =>
            new P102BinaryTreeLevelOrderTraversal.Solution().LevelOrder(root).Reverse().ToList();
            // 效率比List的Reverse稍微低一点
    }

    // abstract
    // public class MultiTest
    // {
    //     protected virtual ISolution GetSo => new Solution();
    //
    //     [Theory]
    //     [InlineData()]
    //     public void Test(int input, int expect)
    //     {
    //         var so = GetSo;
    //         var result = so
    //         Assert.Equal(expect, result);
    //     }
    // }
    // public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
