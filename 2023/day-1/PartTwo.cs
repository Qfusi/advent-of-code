namespace day_1;

public static class PartTwo
{
    private static readonly IReadOnlyDictionary<string, char> _map = new Dictionary<string, char>
    {
        { "one", '1' },
        { "two", '2' },
        { "three", '3' },
        { "four", '4' },
        { "five", '5' },
        { "six", '6' },
        { "seven", '7' },
        { "eight", '8' },
        { "nine", '9' },
        { "1", '1' },
        { "2", '2' },
        { "3", '3' },
        { "4", '4' },
        { "5", '5' },
        { "6", '6' },
        { "7", '7' },
        { "8", '8' },
        { "9", '9' }
    };

    public static int GetCalibrationSum(string[] lines)
    {
        int calibrationSum = 0;
        foreach (var line in lines)
        {
            (int? i, char c) first = (null, '!');
            (int? i, char c) last = (null, '!');
            foreach (var m in _map)
            {
                var firstIndex = line.IndexOf(m.Key);
                var lastIndex = line.LastIndexOf(m.Key);

                if (firstIndex is not -1 && (first.i is null || firstIndex < first.i))
                    first = (firstIndex, m.Value);
                if (lastIndex is not -1 && (last.i is null || lastIndex > last.i))
                    last = (lastIndex, m.Value); ;
            }
            calibrationSum += int.Parse([first.c, last.c]);
        }
        return calibrationSum;
    }
}
