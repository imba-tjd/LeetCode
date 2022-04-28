using LeetCode.DataStructures;

namespace LeetCode.Problems.P637AverageofLevelsinBinaryTree
{
    public interface ISolution { IList<double> AverageOfLevels(TreeNode root); }
    class Solution : ISolution
    {
        public IList<double> AverageOfLevels(TreeNode root)
        {
            var l = new List<double>();
            if (root == null)
                return l;

            var q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int len = q.Count;
                double total = 0;
                for (int i = 0; i < len; i++)
                {
                    var c = q.Dequeue();
                    total += c.val;
                    if (c.left != null)
                        q.Enqueue(c.left);
                    if (c.right != null)
                        q.Enqueue(c.right);
                }
                l.Add(total / len);
            }

            return l;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution So => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new int?[] { 3, 9, 20, null, null, 15, 7 }, new[] { 3, 14.5, 11 } };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int?[] treearr, double[] expect)
        {
            var tree = TreeNodeHelper.Create(treearr);
            var result = So.AverageOfLevels(tree);
            Assert.Equal(expect, result);
        }
    }
    // public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
    // public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
}
