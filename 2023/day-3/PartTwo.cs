using System.Text.RegularExpressions;

namespace day_3;

public static partial class PartTwo
{
    private static Regex _partRegex = GetPartRegex();
    private static Regex _gearRegex = GetGearRegex();

    public static int GetGearRatioSum(string[] lines)
    {
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var gearIndexes = _gearRegex.Matches(lines.ElementAt(i)).Where(x => x.Success).Select(x => x.Index);
            if (!gearIndexes.Any())
                continue;

            var parts = GetPartRanges(lines.ElementAt(i), lines.ElementAtOrDefault(i - 1), lines.ElementAtOrDefault(i + 1));

            foreach (var gearIndex in gearIndexes)
            {
                if (TryGetGearRatio(gearIndex, parts, out var gearRatio))
                {
                    sum += gearRatio;
                }
            }
        }

        return sum;
    }

    private static bool TryGetGearRatio(int gearIndex, IEnumerable<(int startIndex, int endIndex, int value)> parts, out int gearRatio)
    {
        gearRatio = 0;
        List<int> gears = new(2);
        foreach (var (startIndex, endIndex, value) in parts)
        {
            if ((gearIndex >= startIndex - 1) && (gearIndex <= endIndex))
                gears.Add(value);

            if (gears.Count == 2)
            {
                gearRatio += gears.ElementAt(0) * gears.ElementAt(1);
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<(int startIndex, int endIndex, int value)> GetPartRanges(string currentLine, string? previousLine, string? nextLine)
    {
        IEnumerable<(int, int, int)> partRanges =
        [
            .. _partRegex.Matches(currentLine).Where(x => x.Success).Select(x => (startIndex: x.Index, endIndex: x.Index + x.Length, value: int.Parse(x.Value))),
            .. previousLine is not null ? _partRegex.Matches(previousLine).Where(x => x.Success).Select(x => (startIndex: x.Index, endIndex: x.Index + x.Length, value: int.Parse(x.Value))) : new List<(int, int, int)>(),
            .. nextLine is not null ? _partRegex.Matches(nextLine).Where(x => x.Success).Select(x => (startIndex: x.Index, endIndex: x.Index + x.Length, value: int.Parse(x.Value))) : new List<(int, int, int)>(),
        ];
        return partRanges;
    }

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex GetPartRegex();
    [GeneratedRegex(@"\*")]
    private static partial Regex GetGearRegex();
}
