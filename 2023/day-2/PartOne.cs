using System.Text.RegularExpressions;

namespace day_2;

public static class PartOne
{
    public static int GetGameSum(string[] lines)
    {
        var idRegex = new Regex(@"(?<=Game )(\d*)(?=:)");
        var groupRegex = new Regex(@"(?:(?<amount>\d+) (?<name>\w+))");
        int sum = 0;

        foreach (var line in lines)
        {
            var gameId = int.Parse(idRegex.Match(line).Value);
            var cubeResults = groupRegex.Matches(line)
                .Select(x => new KeyValuePair<string, int>(x.Groups["name"].Value, int.Parse(x.Groups["amount"].Value)))
                .GroupBy(x => x.Key)
                .Select(y => new { name = y.Key, value = y.Max(x => x.Value) })
                .Distinct()
                .ToDictionary(x => x.name, x => x.value);

            bool gameIsPossible = true;
            foreach (var result in cubeResults)
            {
                if ((result.Key == "red" && result.Value > 12) ||
                    (result.Key == "green" && result.Value > 13) ||
                    (result.Key == "blue" && result.Value > 14))
                {
                    gameIsPossible = false;
                    break;
                }
            }

            if (!gameIsPossible)
                continue;

            sum += gameId;
        }

        return sum;
    }
}
