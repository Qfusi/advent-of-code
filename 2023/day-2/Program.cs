using day_2;

var lines = File.ReadAllLines("input.txt");

// var sum = PartOne.GetGameSum(lines);
var sum = PartTwo.GetGameSum(lines);

Console.WriteLine($"{Environment.NewLine}sum: {sum}");