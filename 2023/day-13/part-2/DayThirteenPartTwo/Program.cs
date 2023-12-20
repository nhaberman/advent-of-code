// See https://aka.ms/new-console-template for more information
using DayThirteenPartTwo;

Console.WriteLine("Running solution for Day 13, part 2...");

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

// calculate the original reflections
patterns.ForEach(item => item.CalculateReflection());

// fix the smudges to get the new reflection
patterns.ForEach(item => item.FixSmudge());

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
Running solution for Day 13, part 2...
Found 100 ground patterns.
Examining pattern...
  Fixed the smudge at row 16, column 8 after 143 attempts.
    The new reflection is at row 16.
Examining pattern...
  Fixed the smudge at row 5, column 12 after 64 attempts.
    The new reflection is at row 5.
Examining pattern...
  Fixed the smudge at row 12, column 8 after 151 attempts.
    The new reflection is at column 9.
Examining pattern...
  Fixed the smudge at row 2, column 14 after 29 attempts.
    The new reflection is at row 2.
Examining pattern...
  Fixed the smudge at row 14, column 8 after 177 attempts.
    The new reflection is at row 14.
Examining pattern...
  Fixed the smudge at row 12, column 2 after 101 attempts.
    The new reflection is at row 13.
Examining pattern...
  Fixed the smudge at row 7, column 2 after 104 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 6, column 12 after 77 attempts.
    The new reflection is at column 12.
Examining pattern...
  Fixed the smudge at row 2, column 5 after 16 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 4, column 16 after 67 attempts.
    The new reflection is at column 16.
Examining pattern...
  Fixed the smudge at row 11, column 13 after 163 attempts.
    The new reflection is at column 13.
Examining pattern...
  Fixed the smudge at row 5, column 1 after 69 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 5, column 2 after 62 attempts.
    The new reflection is at column 5.
Examining pattern...
  Fixed the smudge at row 10, column 14 after 167 attempts.
    The new reflection is at column 15.
Examining pattern...
  Fixed the smudge at row 2, column 7 after 22 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 8, column 2 after 93 attempts.
    The new reflection is at column 6.
Examining pattern...
  Fixed the smudge at row 7, column 13 after 103 attempts.
    The new reflection is at row 8.
Examining pattern...
  Fixed the smudge at row 6, column 10 after 65 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 4, column 15 after 60 attempts.
    The new reflection is at row 6.
Examining pattern...
  Fixed the smudge at row 3, column 3 after 21 attempts.
    The new reflection is at row 3.
Examining pattern...
  Fixed the smudge at row 9, column 3 after 75 attempts.
    The new reflection is at row 10.
Examining pattern...
  Fixed the smudge at row 6, column 4 after 89 attempts.
    The new reflection is at column 4.
Examining pattern...
  Fixed the smudge at row 14, column 9 after 230 attempts.
    The new reflection is at row 14.
Examining pattern...
  Fixed the smudge at row 3, column 10 after 32 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 2, column 3 after 20 attempts.
    The new reflection is at row 2.
Examining pattern...
  Fixed the smudge at row 3, column 11 after 45 attempts.
    The new reflection is at row 5.
Examining pattern...
  Fixed the smudge at row 13, column 6 after 210 attempts.
    The new reflection is at column 7.
Examining pattern...
  Fixed the smudge at row 5, column 10 after 62 attempts.
    The new reflection is at column 11.
Examining pattern...
  Fixed the smudge at row 9, column 5 after 109 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 11, column 11 after 161 attempts.
    The new reflection is at column 12.
Examining pattern...
  Fixed the smudge at row 5, column 12 after 64 attempts.
    The new reflection is at column 12.
Examining pattern...
  Fixed the smudge at row 10, column 7 after 124 attempts.
    The new reflection is at row 11.
Examining pattern...
  Fixed the smudge at row 2, column 14 after 29 attempts.
    The new reflection is at column 14.
Examining pattern...
  Fixed the smudge at row 1, column 6 after 6 attempts.
    The new reflection is at column 6.
Examining pattern...
  Fixed the smudge at row 6, column 3 after 68 attempts.
    The new reflection is at column 6.
Examining pattern...
  Fixed the smudge at row 10, column 4 after 103 attempts.
    The new reflection is at row 12.
Examining pattern...
  Fixed the smudge at row 9, column 9 after 145 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 4, column 5 after 32 attempts.
    The new reflection is at row 5.
Examining pattern...
  Fixed the smudge at row 13, column 12 after 192 attempts.
    The new reflection is at row 13.
Examining pattern...
  Fixed the smudge at row 7, column 3 after 69 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 3, column 6 after 32 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 6, column 2 after 77 attempts.
    The new reflection is at column 2.
Examining pattern...
  Fixed the smudge at row 11, column 5 after 95 attempts.
    The new reflection is at column 6.
Examining pattern...
  Fixed the smudge at row 2, column 7 after 18 attempts.
    The new reflection is at row 2.
Examining pattern...
  Fixed the smudge at row 10, column 6 after 159 attempts.
    The new reflection is at row 10.
Examining pattern...
  Fixed the smudge at row 11, column 11 after 121 attempts.
    The new reflection is at row 11.
Examining pattern...
  Fixed the smudge at row 5, column 3 after 63 attempts.
    The new reflection is at column 7.
Examining pattern...
  Fixed the smudge at row 1, column 10 after 10 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 5, column 12 after 64 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 6, column 1 after 46 attempts.
    The new reflection is at row 9.
Examining pattern...
  Fixed the smudge at row 9, column 7 after 111 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 1, column 5 after 5 attempts.
    The new reflection is at row 1.
Examining pattern...
  Fixed the smudge at row 13, column 8 after 116 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 6, column 3 after 58 attempts.
    The new reflection is at column 6.
Examining pattern...
  Fixed the smudge at row 5, column 2 after 38 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 3, column 1 after 31 attempts.
    The new reflection is at row 4.
Examining pattern...
  Fixed the smudge at row 10, column 2 after 119 attempts.
    The new reflection is at row 10.
Examining pattern...
  Fixed the smudge at row 14, column 1 after 222 attempts.
    The new reflection is at column 4.
Examining pattern...
  Fixed the smudge at row 1, column 2 after 2 attempts.
    The new reflection is at row 2.
Examining pattern...
  Fixed the smudge at row 3, column 7 after 25 attempts.
    The new reflection is at column 7.
Examining pattern...
  Fixed the smudge at row 11, column 5 after 175 attempts.
    The new reflection is at column 5.
Examining pattern...
  Fixed the smudge at row 8, column 5 after 96 attempts.
    The new reflection is at column 7.
Examining pattern...
  Fixed the smudge at row 7, column 1 after 55 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 1, column 15 after 15 attempts.
    The new reflection is at column 15.
Examining pattern...
  Fixed the smudge at row 5, column 11 after 55 attempts.
    The new reflection is at row 7.
Examining pattern...
  Fixed the smudge at row 1, column 3 after 3 attempts.
    The new reflection is at row 1.
Examining pattern...
  Fixed the smudge at row 6, column 4 after 39 attempts.
    The new reflection is at row 6.
Examining pattern...
  Fixed the smudge at row 1, column 10 after 10 attempts.
    The new reflection is at column 11.
Examining pattern...
  Fixed the smudge at row 6, column 6 after 81 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 2, column 1 after 18 attempts.
    The new reflection is at column 3.
Examining pattern...
  Fixed the smudge at row 1, column 5 after 5 attempts.
    The new reflection is at row 1.
Examining pattern...
  Fixed the smudge at row 11, column 5 after 175 attempts.
    The new reflection is at column 7.
Examining pattern...
  Fixed the smudge at row 6, column 4 after 39 attempts.
    The new reflection is at row 6.
Examining pattern...
  Fixed the smudge at row 2, column 5 after 18 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 12, column 3 after 146 attempts.
    The new reflection is at column 3.
Examining pattern...
  Fixed the smudge at row 5, column 16 after 84 attempts.
    The new reflection is at row 5.
Examining pattern...
  Fixed the smudge at row 10, column 1 after 118 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 6, column 6 after 51 attempts.
    The new reflection is at row 9.
Examining pattern...
  Fixed the smudge at row 8, column 1 after 50 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 2, column 6 after 23 attempts.
    The new reflection is at column 11.
Examining pattern...
  Fixed the smudge at row 1, column 2 after 2 attempts.
    The new reflection is at row 6.
Examining pattern...
  Fixed the smudge at row 11, column 12 after 162 attempts.
    The new reflection is at column 13.
Examining pattern...
  Fixed the smudge at row 1, column 1 after 1 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 3, column 4 after 26 attempts.
    The new reflection is at row 3.
Examining pattern...
  Fixed the smudge at row 2, column 5 after 18 attempts.
    The new reflection is at row 4.
Examining pattern...
  Fixed the smudge at row 6, column 3 after 88 attempts.
    The new reflection is at column 8.
Examining pattern...
  Fixed the smudge at row 8, column 11 after 130 attempts.
    The new reflection is at column 13.
Examining pattern...
  Fixed the smudge at row 1, column 12 after 12 attempts.
    The new reflection is at column 12.
Examining pattern...
  Fixed the smudge at row 10, column 7 after 160 attempts.
    The new reflection is at column 11.
Examining pattern...
  Fixed the smudge at row 1, column 2 after 2 attempts.
    The new reflection is at row 1.
Examining pattern...
  Fixed the smudge at row 8, column 4 after 95 attempts.
    The new reflection is at row 10.
Examining pattern...
  Fixed the smudge at row 12, column 14 after 179 attempts.
    The new reflection is at row 13.
Examining pattern...
  Fixed the smudge at row 8, column 1 after 78 attempts.
    The new reflection is at row 8.
Examining pattern...
  Fixed the smudge at row 1, column 11 after 11 attempts.
    The new reflection is at row 2.
Examining pattern...
  Fixed the smudge at row 5, column 3 after 63 attempts.
    The new reflection is at column 4.
Examining pattern...
  Fixed the smudge at row 1, column 7 after 7 attempts.
    The new reflection is at column 11.
Examining pattern...
  Fixed the smudge at row 10, column 1 after 100 attempts.
    The new reflection is at row 11.
Examining pattern...
  Fixed the smudge at row 16, column 1 after 226 attempts.
    The new reflection is at column 1.
Examining pattern...
  Fixed the smudge at row 9, column 10 after 98 attempts.
    The new reflection is at column 10.
Examining pattern...
  Fixed the smudge at row 7, column 8 after 74 attempts.
    The new reflection is at row 7.
Answer is:  32728
*/

/*
20808 -> too low
35044 -> too high
32728 -> correct!
*/