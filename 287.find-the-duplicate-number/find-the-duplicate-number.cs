using Xunit;

namespace LeetCode.Problems.P287FindtheDuplicateNumber
{
    public interface ISolution { int FindDuplicate(int[] nums); }
    class Solution : ISolution
    {
        // BF二重循环，空间O(1)，时间O(n^2)
        public int FindDuplicate(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
                for (int j = i + 1; j < nums.Length; j++)
                    if (nums[i] == nums[j])
                        return nums[i];
            throw new Exception("Not possible");
        }
    }

    class Solution2 : ISolution
    {
        // 哈希表，空间O(n)，时间O(n)
        public int FindDuplicate(int[] nums)
        {
            var hs = new HashSet<int>();
            foreach (int n in nums)
                if (hs.Contains(n))
                    return n;
                else
                    hs.Add(n);
            throw new Exception("Not possible");
        }
    }

    class Solution3 : ISolution
    {
        // 排序，如果是堆排，空间O(1)，时间O(nlogn)；但要改变原数组，否则空间不为O(1)
        public int FindDuplicate(int[] nums)
        {
            Array.Sort(nums);
            for (int i = 0; i < nums.Length - 1; i++)
                if (nums[i] == nums[i + 1])
                    return nums[i];
            throw new Exception("Not possible");
        }
    }

    class Solution4 : ISolution
    {
        // Floyd's Tortoise and Hare (Cycle Detection)
        public int FindDuplicate(int[] nums)
        {
            int slow, fast;
            slow = fast = 0;

            do
            {
                // 根据题意，一定存在环；否则还要考虑长度小于3的特殊情形
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (nums[slow] != nums[fast]);

            int third = 0;
            while (nums[slow] != nums[third]) // 不能是slow != third，不知道为什么
                (slow, third) = (nums[slow], nums[third]);

            return nums[slow];
        }
    }

    class Solution5 : ISolution
    {
        public int FindDuplicate(int[] nums)
        {
            int from = 1, to = nums.Length - 1; // 代表数字范围而不是索引

            while (from < to)
            {
                int mid = from + (to - from) / 2;
                // 如果没有重复的元素，[from,mid]每个数都有且只有一个，数量等于mid-form+1
                if (Count(nums, from, mid) > mid - from + 1) // 必须是大于而不是不等于，因为一边数量大，另一边就小，都是不等于
                    to = mid; // 在[from,mid]中
                else
                    from = mid + 1; // 在[mid+1,to]中
            }

            return from; // from==to，本身就是重复的数字
        }

        int Count(int[] nums, int from, int to) // 统计数组中有多少个元素在[from,to]之中
        {
            int count = 0;
            foreach (int n in nums)
                if (from <= n && n <= to)
                    count++;
            return count;
        }
    }

    // abstract
    public class MultiTest
    {
        protected virtual ISolution GetSo => new Solution5();

        [Theory]
        // [InlineData(new[] { 1, 3, 4, 2, 2 }, 2)]
        // [InlineData(new[] { 3, 1, 3, 4, 2 }, 3)]
        [InlineData(new[] { 1, 4, 6, 6, 6, 2, 3 }, 6)]
        public void Test(int[] input, int expect)
        {
            var so = GetSo;
            var result = so.FindDuplicate(input);
            Assert.Equal(expect, result);
        }
    }
    public class Test1 : MultiTest { protected override ISolution GetSo => new Solution(); }
    public class Test2 : MultiTest { protected override ISolution GetSo => new Solution2(); }
    public class Test3 : MultiTest { protected override ISolution GetSo => new Solution3(); }
    public class Test4 : MultiTest { protected override ISolution GetSo => new Solution4(); }
    public class Test5 : MultiTest { protected override ISolution GetSo => new Solution5(); }
}
