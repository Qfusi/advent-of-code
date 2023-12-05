using day_3;

var lines = File.ReadAllLines("input.txt");

// var sum = PartOne.GetEnginePartSum(lines);
var sum = PartTwo.GetGearRatioSum(lines);

Console.WriteLine($"{Environment.NewLine}sum: {sum}");