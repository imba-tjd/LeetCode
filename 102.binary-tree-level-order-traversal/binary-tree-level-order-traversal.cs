using LeetCode.DataStructures;

namespace LeetCode.Problems.P102BinaryTreeLevelOrderTraversal
{
    public interface ISolution { IList<IList<int>> LevelOrder(TreeNode root); }
    class Solution : ISolution
    {
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var l = new List<IList<int>>();
            if (root == null)
                return l;

            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                var ll = new List<int>();

                int n = q.Count;
                for (int i = 0; i < n; i++)
                {
                    var c = q.Dequeue();
                    ll.Add(c.val);
                    if (c.left != null)
                        q.Enqueue(c.left);
                    if (c.right != null)
                        q.Enqueue(c.right);
                }

                l.Add(ll);
            }

            return l;
        }
    }

    // abstract
    // public class MultiTest
    // {
    //     protected virtual ISolution So => new Solution();

    //     public static IEnumerable<object[]> TestData()
    //     {
    //         yield return new object[] { new int?[] { 3, 9, 20, null, null, 15, 7 }, };
    //     }

    //     [Theory]
    //     [MemberData(nameof(TestData))]
    //     public void Test(int?[] treearr, int[] expect)
    //     {
    //         var tree = TreeNodeHelper.Create(treearr);
    //         var result = So.LevelOrder(tree);
    //         Assert.Equal(expect, result);
    //     }
    // }
    // public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
