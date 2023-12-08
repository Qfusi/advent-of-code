
namespace day_8;

public class Solution
{
    private readonly string _instructions;
    private readonly Dictionary<string, (string, string)> _nodes;

    public Solution(string input)
    {
        var sections = input.Split("\n\r", StringSplitOptions.TrimEntries);
        _instructions = sections[0];
        _nodes = sections[1]
            .Split('\n')
            .Select(x => new KeyValuePair<string, (string, string)>(x[..3], (x[7..10], x[12..15]))).ToDictionary();
    }

    internal long PartOne() => GetPathLength("AAA", "ZZZ");

    internal long PartTwo()
    {
        var starts = _nodes.Keys.Where(k => k[2] == 'A');
        var paths = starts.Select(p => GetPathLength(p, "Z")).ToList();
        return paths.Skip(1).Aggregate(paths[0], GetLcm);
    }

    private static long GetLcm(long a, long b) => a * b / GetGcd(a, b);

    private static long GetGcd(long a, long b)
    {
        long remainder;
        while (b != 0)
        {
            remainder = a % b;
            a = b;
            b = remainder;
        }
        return a;
    }

    private long GetPathLength(string start, string end)
    {
        var pathLength = 0;
        var node = start;
        int currentInstruction = 0;
        while (!node.EndsWith(end))
        {
            node = _instructions[currentInstruction] == 'L' ? _nodes[node].Item1 : _nodes[node].Item2;
            currentInstruction = (currentInstruction + 1) % _instructions.Length;
            ++pathLength;
        }
        return pathLength;
    }
}
