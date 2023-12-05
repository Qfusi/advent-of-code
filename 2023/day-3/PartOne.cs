using System.Text.RegularExpressions;

namespace day_3;

public static partial class PartOne
{
    private static Regex _partRegex = GetPartRegex();
    private static Regex _symbolRegex = GetSymbolRegex();

    public static int GetEnginePartSum(string[] lines)
    {
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var partMatches = _partRegex.Matches(lines.ElementAt(i)).Where(x => x.Success);
            if (!partMatches.Any())
                continue;

            var symbolIndexes = GetSymbolIndexes(lines.ElementAt(i), lines.ElementAtOrDefault(i - 1), lines.ElementAtOrDefault(i + 1));

            foreach (var part in partMatches)
            {
                foreach (var symbolIndex in symbolIndexes)
                {
                    if ((symbolIndex >= part.Index - 1) &&
                        (symbolIndex <= (part.Index + part.Length)))
                    {
                        sum += int.Parse(part.Value);
                        break;
                    }
                }
            }
        }

        return sum;
    }

    private static IEnumerable<int> GetSymbolIndexes(string currentLine, string? previousLine, string? nextLine)
    {
        IEnumerable<int> symbolIndexes =
        [
            .. _symbolRegex.Matches(currentLine).Where(x => x.Success).Select(x => x.Index),
            .. previousLine is not null ? _symbolRegex.Matches(previousLine).Where(x => x.Success).Select(x => x.Index) : new List<int>(),
            .. nextLine is not null ? _symbolRegex.Matches(nextLine).Where(x => x.Success).Select(x => x.Index) : new List<int>(),
        ];
        return symbolIndexes.Distinct();
    }

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex GetPartRegex();
    [GeneratedRegex(@"[^.\d]")]
    private static partial Regex GetSymbolRegex();
}