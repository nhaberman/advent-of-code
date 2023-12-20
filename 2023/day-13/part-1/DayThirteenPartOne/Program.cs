// See https://aka.ms/new-console-template for more information
using DayThirteenPartOne;

Console.WriteLine("Running solution for Day 13, part 1...");

// get file contents
//var input = File.ReadAllLines("../../input-13-demo.txt");
var input = File.ReadAllLines("../../input-13.txt");

// collect the ground patterns
var patterns = new List<Ground>();
List<string> inputLines = new();
foreach (string line in input)
{
    // for empty lines, clear the buffer and create the pattern object
    if (string.IsNullOrEmpty(line))
    {
        Ground groundPattern = new(inputLines);
        patterns.Add(groundPattern);

        inputLines.Clear();
    }
    else
    {
        inputLines.Add(line);
    }
}

if (inputLines.Any())
{
    Ground groundPattern = new(inputLines);
    patterns.Add(groundPattern);
}

Console.WriteLine($"Found {patterns.Count} ground patterns.");

// calculate the reflections
patterns.ForEach(item => item.CalculateReflection());

// calculate the answer
int answer = 0;

foreach (var pattern in patterns)
{
    if (pattern.IsRowReflection ?? false)
    {
        answer += (pattern.ReflectionLine ?? 0) * 100;
    }
    else if (pattern.IsColumnReflection ?? false)
    {
        answer += pattern.ReflectionLine ?? 0;
    }
}

Console.WriteLine($"Answer is:  {answer}");

/*
Running solution for Day 13, part 1...
Found 100 ground patterns.
Answer is:  27742
*/