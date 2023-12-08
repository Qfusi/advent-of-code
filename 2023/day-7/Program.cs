using day_7;

var input = File.ReadAllText("input.txt");

var solution = new Solution();
var result = solution.Solve_PartOne(input);
var result2 = solution.Solve_PartTwo(input);

Console.WriteLine();
Console.WriteLine($"part one: {result}");
Console.WriteLine($"part two: {result2}");
