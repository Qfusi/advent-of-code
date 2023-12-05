using System.Text.RegularExpressions;

namespace day_2;

public static class PartTwo
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

            int minimumCubeSetProduct = 1;
            foreach (var result in cubeResults)
                minimumCubeSetProduct *= result.Value;

            sum += minimumCubeSetProduct;
        }

        return sum;
    }
}
