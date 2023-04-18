namespace LeetCode.Problems.P160IntersectionofTwoLinkedLists;
using LeetCode.DataStructures;

public interface ISolution { ListNode GetIntersectionNode(ListNode headA, ListNode headB); }
class Solution : ISolution
{
    int Count(ListNode node) => node == null ? 0 : Count(node.next) + 1;
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        int countA = Count(headA);
        int countB = Count(headB);

        ListNode longLN, shortLN;
        int diff; // 长度之差
        if (countA > countB)
        {
            longLN = headA;
            shortLN = headB;
            diff = countA - countB;
        }
        else
        {
            longLN = headB;
            shortLN = headA;
            diff = countB - countA;
        }

        while (--diff >= 0) // 去掉长的部分
            longLN = longLN.next;

        while (longLN != null) // 同步往下走，遇到相同的就是了，遇到null就是走完了也没有相同的
        {
            if (longLN == shortLN)
                return longLN;
            longLN = longLN.next;
            shortLN = shortLN.next;
        }

        return null;
    }
}

// abstract
// public class MultiTest
// {
//     protected virtual ISolution So => new Solution();

//     [Theory]
//     [InlineData(8, new[]{4,1,8,4,5}, new[]{5,6,1,8,4,5})]
//     // [InlineData(8, new[]{4,1,8,4,5}, new[]{5,6,1,8,4,5}, 2, 3)]
//     public void Test(int intersectVal, int[] listA, int[] listB)
//     // public void Test(int intersectVal, int[] listA, int[] listB, int skipA, int skipB)
//     {
//         ListNode headA = ListNodeHelper.Create(listA);
//         ListNode headB = ListNodeHelper.Create(listB);

//         var result = So.GetIntersectionNode(headA, headB);
//         Assert.NotNull(result);
//         Assert.Equal(intersectVal, result.val);
//     }
// }
// public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
// public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
