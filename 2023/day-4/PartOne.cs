namespace day_4;

public static class PartOne
{
    public static int GetScratchSum(string[] scratchCards)
    {
        return scratchCards.Sum(x => GetCardPoints(x[8..]));
    }

    private static int GetCardPoints(string card)
    {
        var sides = card.Split(" | ");
        var winners = sides.ElementAt(0).Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
        var numbers = sides.ElementAt(1).Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));

        var winCount = numbers.Count(x => winners.Contains(x));
        if (winCount == 0)
            return 0;

        var points = 1;
        return points <<= winCount - 1;
    }
}
