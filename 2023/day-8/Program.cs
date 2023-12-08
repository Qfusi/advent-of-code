using day_8;

var input = File.ReadAllText("input.txt");

var solution = new Solution(input);
var result = solution.PartOne();
var result2 = solution.PartTwo();

// Console.WriteLine();
Console.WriteLine($"part one: {result}");
Console.WriteLine($"part two: {result2}");
