namespace day_11;

public static class Solution
{
    internal static long PartOne(string[] input) => Solve(input, 1);
    internal static long PartTwo(string[] input) => Solve(input, 9999999);

    private static long Solve(string[] input, int expansionMultiplier)
    {
        var galaxies = ParseGalaxies(input);
        ExpandUniverse(galaxies, expansionMultiplier);
        return galaxies.SelectMany((g1, i) => galaxies.Skip(i + 1).Select(g2 => GetPointsDistance(g1, g2))).Sum();
    }

    static List<(int y, int x)> ParseGalaxies(string[] input)
        => [..input
            .SelectMany((line, y) => line.Select((c, x) => (c, y, x)))
            .Where(t => t.c == '#').Select(t => (t.y, t.x))];

    static long GetPointsDistance((int y, int x) a, (int y, int x) b)
        => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

    static void ExpandUniverse(List<(int y, int x)> galaxies, int expansionMultiplier)
    {
        var emptyColumns = Enumerable.Range(0, galaxies.Max(g => g.x)).Where(x => galaxies.All(g => g.x != x)).Select(x => x);
        var emptyRows = Enumerable.Range(0, galaxies.Max(g => g.y)).Where(y => galaxies.All(g => g.y != y)).Select(y => y);
        galaxies
            .Select((g, i) => new { i, position = g, colCount = emptyColumns.Count(x => g.x > x), rowCount = emptyRows.Count(y => g.y > y) })
            .Where(g => g.rowCount > 0 || g.colCount > 0)
            .Select(g => new
            {
                g.i,
                x = g.colCount != 0 ? g.position.x + g.colCount * expansionMultiplier : g.position.x,
                y = g.rowCount != 0 ? g.position.y + g.rowCount * expansionMultiplier : g.position.y
            }).ToList().ForEach(x => galaxies[x.i] = new(x.y, x.x));
    }
}