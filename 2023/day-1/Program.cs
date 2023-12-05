using day_1;

var lines = File.ReadAllLines("input.txt");

// var calibrationSum = PartOne.GetCalibrationSum(lines);
var calibrationSum = PartTwo.GetCalibrationSum(lines);

Console.WriteLine(calibrationSum);