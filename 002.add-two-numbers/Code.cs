using LeetCode.DataStructures;

namespace LeetCode.Problems.P002AddTwoNumbers;

class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) => AddInternal(l1, l2, 0);
    ListNode AddInternal(ListNode l1, ListNode l2, int carry)
    {
        int t = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        return l1 == null && l2 == null && carry == 0 ? null :
            new ListNode(t % 10, AddInternal(l1?.next, l2?.next, t / 10));
    }
}

class Solution2 // 常规解法
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var l3 = new ListNode(-1);
        var p = l3;
        int carry = 0;
        while (l1 != null || l2 != null) // 变化1：加 || carry!=0 则最后不用额外判断，但多分支实际会降低速度
        {
            int val = carry;
            if (l1 != null) // 变化2：上面的while改成l1和l2均不为null，则此处不必判断；后面再加两个循环分别处理二者其一不为null
            {
                val += l1.val;
                l1 = l1.next;
            }
            if (l2 != null)
            {
                val += l2.val;
                l2 = l2.next;
            }
            if (val > 9) // 变化3：不用carry变量，只用val，当前值为 val%10，下一次的值为 val/=10。但实际会降低速度
            {
                val -= 10;
                carry = 1;
            }
            else
                carry = 0;

            p.next = new ListNode(val);
            p = p.next;
        }
        if (carry != 0)
            p.next = new ListNode(1);
        return l3.next;
    }
}

public class UnitTest
{
    [Theory]
    [InlineData(1, 2, new[] { 3 })]
    [InlineData(342, 465, new[] { 7, 0, 8 })]
    [InlineData(3, 9, new[] { 2, 1 })]
    void Test(int a, int b, int[] expects)
    {
        ListNode l1 = ListNodeHelper.Create(a);
        ListNode l2 = ListNodeHelper.Create(b);
        var solution = new Solution();

        ListNode r = solution.AddTwoNumbers(l1, l2);
        foreach (int n in expects)
        {
            Assert.Equal(n, r.val);
            r = r.next;
        }
        Assert.Null(r);
    }
}
