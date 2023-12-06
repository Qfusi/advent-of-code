
using day_5;

var input = File.ReadAllText("input.txt");

var solution = new Solution();
var min = solution.Solve_PartOne(input);
var min2 = solution.Solve_PartTwo(input);

System.Console.WriteLine($"part one: {min}");
System.Console.WriteLine($"part two: {min2}");