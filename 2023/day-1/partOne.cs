namespace day_1;

public static class PartOne
{
    public static int GetCalibrationSum(string[] lines)
    {
        int calibrationSum = 0;
        foreach (var line in lines)
        {
            ReadOnlySpan<char> chars =
            [
                line.FirstOrDefault(char.IsDigit),
                line.LastOrDefault(char.IsDigit)
            ];
            calibrationSum += int.Parse(chars);
        }
        return calibrationSum;
    }
}
