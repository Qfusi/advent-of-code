using day_9;

var input = File.ReadAllLines("input.txt");

// var solution = new Solution(input);
var result = Solution.PartOne(input);
var result2 = Solution.PartTwo(input);

Console.WriteLine($"part one: {result}");
Console.WriteLine($"part two: {result2}");
