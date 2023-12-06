
using System.Text.RegularExpressions;

namespace day_5;

public class Solution
{

    public long Solve_PartOne(string input) => Solve(input, ParsePartOneSeeds);

    public long Solve_PartTwo(string input) => Solve(input, ParsePartTwoSeeds);

    private long Solve(string input, Func<long[], Range[]> parseSeeds)
    {
        var blocks = input.Split("\n\r", StringSplitOptions.TrimEntries);
        var seeds = parseSeeds(GetSeedNumbers(blocks[0]));
        var maps = blocks[1..].Select(ParseMap).ToArray();

        return maps.Aggregate(seeds, Project).Select(r => r.From).Min();
    }

    private Range[] Project(Range[] seedInput, Dictionary<Range, Range> map)
    {
        var queue = new Queue<Range>();
        foreach (var seed in seedInput)
            queue.Enqueue(seed);

        var outputSeeds = new List<Range>();
        while (queue.Count != 0)
        {
            var range = queue.Dequeue();
            var source = map.Keys.FirstOrDefault(src => Intersects(src, range));

            // If no entry intersects our range -> just add it to the output. 
            if (source == null)
            {
                outputSeeds.Add(range);
            }
            // If an entry completely contains the range -> add after mapping.
            else if (source.From <= range.From && range.To <= source.To)
            {
                var destination = map[source];
                var shift = destination.From - source.From;
                outputSeeds.Add(new Range(range.From + shift, range.To + shift));
            }
            // these two are only used for PartTwo
            // Otherwise, some entry partly covers the range. In this case 'chop' 
            // the range into two halfs getting rid of the intersection. The new 
            // pieces are added back to the queue for further processing and will be 
            // ultimately consumed by the first two cases.
            else if (range.From < source.From)
            {
                queue.Enqueue(new Range(range.From, source.From - 1));
                queue.Enqueue(new Range(source.From, range.To));
            }
            else
            {
                queue.Enqueue(new Range(range.From, source.To));
                queue.Enqueue(new Range(source.To + 1, range.To));
            }
        }
        return [.. outputSeeds];
    }

    private static bool Intersects(Range r1, Range r2) => r1.From <= r2.To && r2.From <= r1.To;

    // consider each number as a range of 1 length
    private Range[] ParsePartOneSeeds(long[] seedNumbers) =>
        [.. from n in seedNumbers select new Range(n, n)];

    // chunk is a great way to iterate over the pairs of numbers
    private Range[] ParsePartTwoSeeds(long[] seedNumbers) =>
        [.. from c in seedNumbers.Chunk(2) select new Range(c[0], c[0] + c[1] - 1)];

    public long[] GetSeedNumbers(string input) =>
        [.. from m in Regex.Matches(input, @"\d+") select long.Parse(m.Value)];

    private Dictionary<Range, Range> ParseMap(string input) =>
        input.Split("\n").Skip(1)
            .Select(x => x.Split(" ").Select(long.Parse).ToArray())
            .Select(x => new
            {
                source = new Range(x[1], x[1] + x[2] - 1),
                destination = new Range(x[0], x[0] + x[2] - 1)
            })
            .Select(x => new KeyValuePair<Range, Range>(x.source, x.destination)).ToDictionary();
}

record Range(long From, long To);