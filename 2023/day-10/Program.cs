using day_10;

var input = File.ReadAllLines("input.txt");


var solution = new Solution();
solution.Setup(input);
var result = solution.PartOne();
// var result2 = Solution.PartTwo(input);

Console.WriteLine($"part one: {result}");
// Console.WriteLine($"part two: {result2}");
