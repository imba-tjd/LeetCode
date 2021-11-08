using LeetCode.DataStructures;
using System.Linq;

namespace LeetCode.Problems.P108ConvertSortedArraytoBinarySearchTree
{
    public interface ISolution { TreeNode SortedArrayToBST(int[] nums); }
    class Solution : ISolution
    {
        public TreeNode SortedArrayToBST(int[] nums) => SortedArrayToBST(nums, 0, nums.Length);
        TreeNode SortedArrayToBST(int[] nums, int from, int to)
        {
            if (to <= from)
                return null;

            int mid = from + (to - from) / 2;

            var tn = new TreeNode(nums[mid]);
            tn.left = SortedArrayToBST(nums, from, mid);
            tn.right = SortedArrayToBST(nums, mid + 1, to);

            return tn;
        }
    }

    class Solution2 : ISolution
    {
        public TreeNode SortedArrayToBST(int[] nums)
        {
            if (nums.Length == 0)
                return null;

            var s = new Stack<(TreeNode, int, int)>();
            var root = new TreeNode(-1);
            s.Push((root, 0, nums.Length));

            while (s.Count != 0)
            {
                var (tn, from, to) = s.Pop();
                int mid = from + (to - from) / 2;
                tn.val = nums[mid];

                if (from < mid)
                    s.Push((tn.left = new TreeNode(-1), from, mid));
                if (mid + 1 < to)
                    s.Push((tn.right = new TreeNode(-1), mid + 1, to));
            }

            return root;
        }
    }

    abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution();

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new[] { -10, -3, 0, 5, 9 }, new int?[] { 0, -3, 9, -10, null, 5 } };
            yield return new object[] { new[] { 7, 10, 13, 16, 19, 29, 32, 33, 37, 41, 43 },
                new int?[] { 29, 13, 37, 10, 19, 33, 43, 7, null, 16, null, 32, null, 41 } };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(int[] arr, int?[] expect)
        {
            var so = GetSo;
            var result = so.SortedArrayToBST(arr);
            Assert.Equal(expect, result.AsEnumerable().ToArray());
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
}
