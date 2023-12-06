namespace day_4;

public static class PartTwo
{
    private static Dictionary<int, int> _cardMap = [];

    public static int GetTotalScratchCards(string[] scratchCards)
    {
        _cardMap = scratchCards.Select((x, i) => new KeyValuePair<int, int>(i, GetCardWinCount(x))).ToDictionary();
        var sum = 0;
        foreach (var card in _cardMap)
            sum += GetCardValue(card);
        return sum;
    }

    private static int GetCardValue(KeyValuePair<int, int> card, int indention = 0)
    {
        var cardCount = 1;
        if (card.Value == 0)
            return cardCount;

        for (int i = card.Key + 1; i <= card.Key + card.Value; i++)
            cardCount += GetCardValue(_cardMap.ElementAt(i), indention + 3);

        return cardCount;
    }

    private static int GetCardWinCount(string card)
    {
        var sides = card.Split(" | ");
        var winners = sides.ElementAt(0).Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
        var numbers = sides.ElementAt(1).Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
        return numbers.Count(x => winners.Contains(x));
    }
}
