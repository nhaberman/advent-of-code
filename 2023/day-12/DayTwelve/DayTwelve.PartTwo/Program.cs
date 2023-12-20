// See https://aka.ms/new-console-template for more information
using DayTwelve.Core;

Console.WriteLine("Running solution for Day 12, part 2...");

// get file contents
var input = File.ReadAllLines("../../input-12-demo.txt");
//var input = File.ReadAllLines("../../input-12.txt");

// collect the spring records
var records = new List<SpringRecord>();
foreach (string line in input)
{
    // unfold the record
    string unfoldedRecord = SpringRecord.UnfoldSpringRecordInput(line);
    
    records.Add(new SpringRecord(unfoldedRecord));

    //records.Add(new SpringRecord(line));
}

Console.WriteLine($"Found {records.Count} spring records.");

// calculate the possible arrangement counts
records.ForEach(item => item.CalculatePossibleArangements2());

// find the total possible arrangements
int totalPossibleArrangements = records.Sum(item => item.TotalValidArrangements);

Console.WriteLine($"Answer is:  {totalPossibleArrangements}");

/*
Running solution for Day 12, part 1...
Found 1000 spring records.
Answer is:  7670
*/