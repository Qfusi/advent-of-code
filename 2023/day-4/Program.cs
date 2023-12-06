using System.Diagnostics;
using day_4;


Stopwatch stopWatch = new();
stopWatch.Start();

var scratchCards = File.ReadAllLines("input.txt");

// var sum = PartOne.GetScratchSum(scratchCards);
var totalCards = PartTwo.GetTotalScratchCards(scratchCards);

stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;

Console.WriteLine($"RunTime: {ts.TotalSeconds}s");

// Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"TotalCards: {totalCards}");