
using System.Buffers;
using CommunityToolkit.HighPerformance;

namespace day_6;

public class Solution
{
    private static readonly SearchValues<char> _searchValues = SearchValues.Create("0123456789");

    public int Solve_PartOne(ReadOnlySpan<char> input)
    {
        var lines = GetLines(input);
        var timeLine = lines.GetRowSpan(0);
        var distanceLine = lines.GetRowSpan(1);
        int currentTimeReadIndex = 0;
        int currentDistanceReadIndex = 0;

        int product = 1;
        int availableTime = 0;
        while (availableTime != -1)
        {
            availableTime = GetNumberPartOne(timeLine, currentTimeReadIndex, out currentTimeReadIndex);
            if (availableTime == -1)
                break;
            var distanceToBeat = GetNumberPartOne(distanceLine, currentDistanceReadIndex, out currentDistanceReadIndex);
            product *= GetProduct(availableTime, distanceToBeat);
        }
        return product;
    }

    public long Solve_PartTwo(ReadOnlySpan<char> input)
    {
        var lines = GetLines(input);
        var availableTime = GetNumberPartTwo(lines.GetRowSpan(0));
        var distanceToBeat = GetNumberPartTwo(lines.GetRowSpan(1));
        return GetProduct(availableTime, distanceToBeat);
    }

    public static ReadOnlySpan2D<char> GetLines(ReadOnlySpan<char> input)
    {
        var lineLength = input.IndexOf('\n') + 1;
        return input.AsSpan2D(input.Length / lineLength, lineLength)[.., 10..^1];
    }

    private static int GetNumberPartOne(ReadOnlySpan<char> input, int startIndex, out int endIndex)
    {
        endIndex = 0;

        var numIndex = input[startIndex..].IndexOfAny(_searchValues);
        if (numIndex == -1)
            return -1;

        var range = input[(startIndex + numIndex)..].IndexOfAnyExcept(_searchValues);
        endIndex = startIndex + numIndex + range;
        return int.Parse(input[startIndex..].Slice(numIndex, range));
    }

    private static long GetNumberPartTwo(ReadOnlySpan<char> input)
    {
        var span = (stackalloc char[20]);
        int i = 0;
        foreach (var c in input)
            if (char.IsDigit(c))
                span[i++] = c;
        return long.Parse(span, System.Globalization.NumberStyles.AllowTrailingWhite);
    }

    private static int GetProduct(long availableTime, long distanceToBeat)
    {
        int count = 0;
        for (int i = 1; i <= availableTime; i++)
        {
            var distance = (availableTime - i) * i;
            if (distance > distanceToBeat)
                count++;
        }
        return count;
    }
}
