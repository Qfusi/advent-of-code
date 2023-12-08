using System.Numerics;

namespace day_7;

public class Solution
{
    internal int Solve_PartOne(string input) => Solve(input, Part1_GetPoints);
    internal int Solve_PartTwo(string input) => Solve(input, Part2_GetPoints);

    private static int Solve(string input, Func<string, BigInteger> getPoints)
    {
        var bids = input.Split('\n').Select(x => new
        {
            hand = x[..x.IndexOf(' ')],
            bid = int.Parse(x[x.IndexOf(' ')..])
        }).OrderBy(x => getPoints(x.hand)).Select(x => x.bid);

        return bids.Select((bid, i) => bid * (i + 1)).Sum();
    }

    private BigInteger Part1_GetPoints(string hand)
    {
        var cardValue = GetCardValue(hand, "23456789TJQKA");
        var suiteValue = GetSuiteValue(hand);
        return cardValue + suiteValue;
    }

    private BigInteger Part2_GetPoints(string hand)
    {
        var replacement = hand
            .Where(x => x != 'J')
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .Select(x => x.Key)
            .FirstOrDefault('J');

        var cardValue = GetCardValue(hand, "J23456789TQKA");
        var suiteValue = GetSuiteValue(hand.Replace('J', replacement));
        return suiteValue + cardValue;
    }

    private static BigInteger GetCardValue(string hand, string cardOrder)
        => new BigInteger(hand.Select(ch => (byte)cardOrder.IndexOf(ch)).Reverse().ToArray());

    private static BigInteger GetSuiteValue(string hand)
        => new BigInteger(hand.Select(ch => (byte)hand.Count(x => x == ch)).Order().ToArray()) << 64;
}
