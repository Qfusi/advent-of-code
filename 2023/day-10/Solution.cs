

using System.Diagnostics;

namespace day_10;

public class Solution
{
    private readonly Dictionary<Point, char> _map = [];

    public void Setup(string[] input)
    {
        for (short i = 0; i < input.Length; i++)
            for (short j = 0; j < input[i].Length; j++)
                _map.Add(new Point(j, i), input[i][j]);
    }

    internal int PartOne() => Solve();
    internal int PartTwo() => 1;

    private int Solve()
    {
        Point start, previous;
        start = previous = _map.FirstOrDefault(x => x.Value == 'S').Key;
        var current = GetStartPoint(start);

        int count = 1;
        while (start != current)
        {
            (current, previous) = Traverse(current, previous);
            count++;
        }

        return count / 2;
    }

    private (Point next, Point previous) Traverse(Point current, Point previous)
    {
        var (p1, p2) = GetConnections(_map[current], current);
        return (previous == p1 ? p2 : p1, current);
    }

    private Point GetStartPoint(Point start)
    {
        var next = _map.FirstOrDefault(p => p.Key.X == start.X && p.Key.Y == start.Y - 1);
        char[] pipes = ['|', '7', 'F'];
        if (pipes.Contains(next.Value))
            return next.Key;

        next = _map.FirstOrDefault(p => p.Key.X == start.X + 1 && p.Key.Y == start.Y);
        pipes = ['-', '7', 'J'];
        if (pipes.Contains(next.Value))
            return next.Key;

        next = _map.FirstOrDefault(p => p.Key.X == start.X + 1 && p.Key.Y == start.Y);
        pipes = ['|', 'L', 'J'];
        if (pipes.Contains(next.Value))
            return next.Key;

        next = _map.FirstOrDefault(p => p.Key.X == start.X + 1 && p.Key.Y == start.Y);
        pipes = ['-', 'L', 'F'];
        if (pipes.Contains(next.Value))
            return next.Key;

        return start;
    }

    private static (Point p1, Point p2) GetConnections(char pipe, Point point)
    {
        return pipe switch
        {
            '|' => (new Point(point.X, point.Y - 1), new Point(point.X, point.Y + 1)),
            '-' => (new Point(point.X - 1, point.Y), new Point(point.X + 1, point.Y)),
            'L' => (new Point(point.X, point.Y - 1), new Point(point.X + 1, point.Y)),
            'J' => (new Point(point.X, point.Y - 1), new Point(point.X - 1, point.Y)),
            '7' => (new Point(point.X - 1, point.Y), new Point(point.X, point.Y + 1)),
            'F' => (new Point(point.X + 1, point.Y), new Point(point.X, point.Y + 1)),
            _ => throw new UnreachableException(@"Shouldn't happen ¯\_(ツ)_/¯")
        };
    }
}

record Point(int X, int Y);