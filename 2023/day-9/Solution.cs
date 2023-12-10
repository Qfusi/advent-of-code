
namespace day_9;

public static class Solution
{
    internal static int PartOne(string[] input) => input.Sum(line => GetNextNumberFromRange(line.Split().Select(int.Parse).ToArray()));
    internal static int PartTwo(string[] input) => input.Sum(line => GetNextNumberFromRange(line.Split().Select(int.Parse).ToArray(), true));

    private static int GetNextNumberFromRange(IReadOnlyList<int> numbers, bool partTwo = false)
    {
        if (numbers.All(x => x == 0))
            return 0;

        var differences = numbers.Skip(1).Select((x, i) => x - numbers[i]).ToArray();
        return partTwo
        ? numbers[0] - GetNextNumberFromRange(differences, partTwo)
        : numbers[^1] + GetNextNumberFromRange(differences);
    }
}
