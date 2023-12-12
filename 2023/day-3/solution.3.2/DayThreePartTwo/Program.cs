// See https://aka.ms/new-console-template for more information
using DayThreePartTwo;
using System.Text.RegularExpressions;

Console.WriteLine("Running solution for Day 3, part 2...");
int answer = 0;
int gearCheckCount = 0;
int gearCount = 0;

// get file contents
var schematic = File.ReadAllLines("../../input-3.txt");

var partNumbers = new List<PartNumber>();
var gears = new List<Gear>();

// find all part numbers
Regex partNumberRegex = new(@"\d+");
for (int i = 0; i < schematic.Length; i++)
{
    MatchCollection matches = partNumberRegex.Matches(schematic[i]);

    foreach (Match match in matches)
    {
        partNumbers.Add(new PartNumber(int.Parse(match.Value), i, match.Index, match.Length));
    }
}

// find all gear numbers
Regex gearRegex = new(@"\*");
for (int i = 0; i < schematic.Length; i++)
{
    MatchCollection matches = gearRegex.Matches(schematic[i]);

    foreach (Match match in matches)
    {
        gears.Add(new Gear(i, match.Index));
    }
}

// check every gear for adjacent parts
foreach (var gear in gears)
{
    Console.WriteLine($"Checking gear {gear}...");

    // collect any part numbers that may be adjacent
    var possiblePartNumbers = partNumbers.Where(partNumber => partNumber.Line >= gear.Line - 1 && partNumber.Line <= gear.Line + 1).ToList();

    // diagonally check all possible part number pairs to see if they are adjacent
    for (int i = 0; i < possiblePartNumbers.Count; i++)
    {
        for (int j = i + 1; j < possiblePartNumbers.Count; j++)
        {   
            Console.WriteLine($"Checking possible part numbers {possiblePartNumbers[i]} and {possiblePartNumbers[j]}...");
            gearCheckCount++;

            // if the part numbers comprise a gear, get the gear ratio and save it to the results
            if (gear.IsGear(possiblePartNumbers[i], possiblePartNumbers[j]))
            {
                Console.WriteLine($"Gear {gear} is a gear!");
                gearCount++;
                int gearRatio = possiblePartNumbers[i].Value * possiblePartNumbers[j].Value;
                answer += gearRatio;
            }
        }
    }
}

Console.WriteLine($"Total gear checks performed:  {gearCheckCount}");
Console.WriteLine($"Total gears found:  {gearCount}");
Console.WriteLine($"Answer is:  {answer}");

/*
Total gear checks performed:  140884
Total gears found:  335
Answer is:  84266818
*/