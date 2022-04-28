namespace LeetCode.Problems.P849MaximizeDistancetoClosestPerson;

public interface ISolution { int MaxDistToClosest(int[] seats); }
class Solution : ISolution
{
    public int MaxDistToClosest(int[] seats)
    {
        int maxd = 0;
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i] == 1)
                continue;

            int left = i - 1;
            while (left >= 0 && seats[left] == 0)
                left--;

            int right = i + 1;
            while (right < seats.Length && seats[right] == 0)
                right++;

            int d = left == -1 ? right - i :
                right == seats.Length ? i - left :
                Math.Min(i - left, right - i);
            if (d > maxd)
                maxd = d;
        }
        return maxd;
    }
}

class Solution2 : ISolution
{
    public int MaxDistToClosest(int[] seats)
    {
        int[] left = new int[seats.Length];
        int[] right = new int[seats.Length];

        int leftd = seats.Length;
        int rightd = seats.Length;

        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i] == 1)
                leftd = 0;
            else
                leftd++;
            left[i] = leftd;
        }

        for (int i = seats.Length - 1; i >= 0; i--)
        {
            if (seats[i] == 1)
                rightd = 0;
            else
                rightd++;
            right[i] = rightd;
        }

        int ans = 0;
        for (int i = 0; i < seats.Length; i++)
            ans = Math.Max(ans, Math.Min(left[i], right[i]));
        return ans;
    }
}

abstract
public class MultiTest
{
    protected virtual ISolution So => new Solution2();

    [Theory]
    [InlineData(new[] { 1, 0, 0, 0, 1, 0, 1 }, 2)]
    [InlineData(new[] { 1, 0, 0, 1 }, 1)]
    [InlineData(new[] { 0, 0, 1 }, 2)]
    [InlineData(new[] { 1, 0, 0, 0 }, 3)]
    public void Test(int[] input, int expect)
    {
        var result = So.MaxDistToClosest(input);
        Assert.Equal(expect, result);
    }
}
public class Test1 : MultiTest { protected override ISolution So => new Solution(); }
public class Test2 : MultiTest { protected override ISolution So => new Solution2(); }
