using day_6;

var input = File.ReadAllText("input.txt").AsSpan();

var solution = new Solution();
var result = solution.Solve_PartOne(input);
var result2 = solution.Solve_PartTwo(input);

Console.WriteLine();
Console.WriteLine($"part one: {result}");
Console.WriteLine($"part two: {result2}");