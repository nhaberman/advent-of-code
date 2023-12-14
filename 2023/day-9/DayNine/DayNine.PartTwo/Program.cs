// See https://aka.ms/new-console-template for more information
using DayNine.Core;

Console.WriteLine("Running solution for Day 9, part 2...");

// get file contents
//var input = File.ReadAllLines("../../input-9-demo.txt");
var input = File.ReadAllLines("../../input-9.txt");

// collect the sequences
var sequenceCollections = new List<DerivedSequenceCollection>();
foreach (string line in input)
{
    var values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(value => int.Parse(value));
    var sequence = new Sequence(values);
    sequenceCollections.Add(new(sequence));
}

Console.WriteLine($"Found {sequenceCollections.Count} sequences.");

// calculate the derived sequences for each sequence
sequenceCollections.ForEach(item => item.CalculateDerivedSequences());

// extrapolate the sequences
int answer = sequenceCollections.Select(item => item.ExtrapolateSequencesBackwards()).Sum();

Console.WriteLine($"Answer is:  {answer}");

/*
Running solution for Day 9, part 2...
Found 200 sequences.
Extrapolated value:  0
Extrapolated value:  -4
Extrapolated value:  -4
Extrapolated value:  8
Extrapolated value:  10
Extrapolated value:  15
Extrapolated value:  0
Extrapolated value:  6
Extrapolated value:  4
Extrapolated value:  -1
Extrapolated value:  2
Extrapolated value:  7
Extrapolated value:  5
Extrapolated value:  4
Extrapolated value:  7
Extrapolated value:  1
Extrapolated value:  -5
Extrapolated value:  5
Extrapolated value:  11
Extrapolated value:  15
Extrapolated value:  10
Extrapolated value:  -1
Extrapolated value:  -2
Extrapolated value:  4
Extrapolated value:  9
Extrapolated value:  13
Extrapolated value:  14
Extrapolated value:  -3
Extrapolated value:  7
Extrapolated value:  10
Extrapolated value:  12
Extrapolated value:  6
Extrapolated value:  -1
Extrapolated value:  -3
Extrapolated value:  -5
Extrapolated value:  9
Extrapolated value:  5
Extrapolated value:  11
Extrapolated value:  5
Extrapolated value:  1
Extrapolated value:  0
Extrapolated value:  8
Extrapolated value:  4
Extrapolated value:  11
Extrapolated value:  -5
Extrapolated value:  -2
Extrapolated value:  15
Extrapolated value:  13
Extrapolated value:  -1
Extrapolated value:  2
Extrapolated value:  -2
Extrapolated value:  1
Extrapolated value:  -4
Extrapolated value:  4
Extrapolated value:  -5
Extrapolated value:  14
Extrapolated value:  13
Extrapolated value:  11
Extrapolated value:  13
Extrapolated value:  -4
Extrapolated value:  9
Extrapolated value:  14
Extrapolated value:  -3
Extrapolated value:  -2
Extrapolated value:  -1
Extrapolated value:  -2
Extrapolated value:  -5
Extrapolated value:  1
Extrapolated value:  3
Extrapolated value:  3
Extrapolated value:  -1
Extrapolated value:  15
Extrapolated value:  4
Extrapolated value:  -5
Extrapolated value:  0
Extrapolated value:  6
Extrapolated value:  11
Extrapolated value:  13
Extrapolated value:  -1
Extrapolated value:  8
Extrapolated value:  7
Extrapolated value:  9
Extrapolated value:  2
Extrapolated value:  -3
Extrapolated value:  10
Extrapolated value:  2
Extrapolated value:  8
Extrapolated value:  9
Extrapolated value:  0
Extrapolated value:  13
Extrapolated value:  4
Extrapolated value:  10
Extrapolated value:  6
Extrapolated value:  2
Extrapolated value:  15
Extrapolated value:  -3
Extrapolated value:  6
Extrapolated value:  10
Extrapolated value:  14
Extrapolated value:  7
Extrapolated value:  -2
Extrapolated value:  -3
Extrapolated value:  1
Extrapolated value:  0
Extrapolated value:  1
Extrapolated value:  -3
Extrapolated value:  15
Extrapolated value:  6
Extrapolated value:  14
Extrapolated value:  2
Extrapolated value:  5
Extrapolated value:  8
Extrapolated value:  13
Extrapolated value:  -4
Extrapolated value:  12
Extrapolated value:  2
Extrapolated value:  2
Extrapolated value:  0
Extrapolated value:  15
Extrapolated value:  8
Extrapolated value:  6
Extrapolated value:  -3
Extrapolated value:  0
Extrapolated value:  4
Extrapolated value:  -5
Extrapolated value:  11
Extrapolated value:  7
Extrapolated value:  3
Extrapolated value:  0
Extrapolated value:  9
Extrapolated value:  -1
Extrapolated value:  -5
Extrapolated value:  -1
Extrapolated value:  15
Extrapolated value:  6
Extrapolated value:  15
Extrapolated value:  -5
Extrapolated value:  4
Extrapolated value:  0
Extrapolated value:  9
Extrapolated value:  12
Extrapolated value:  14
Extrapolated value:  1
Extrapolated value:  -3
Extrapolated value:  -1
Extrapolated value:  12
Extrapolated value:  4
Extrapolated value:  -2
Extrapolated value:  1
Extrapolated value:  0
Extrapolated value:  12
Extrapolated value:  7
Extrapolated value:  15
Extrapolated value:  8
Extrapolated value:  12
Extrapolated value:  10
Extrapolated value:  -2
Extrapolated value:  -1
Extrapolated value:  15
Extrapolated value:  3
Extrapolated value:  5
Extrapolated value:  12
Extrapolated value:  -2
Extrapolated value:  -3
Extrapolated value:  1
Extrapolated value:  7
Extrapolated value:  -1
Extrapolated value:  15
Extrapolated value:  11
Extrapolated value:  6
Extrapolated value:  -1
Extrapolated value:  -4
Extrapolated value:  6
Extrapolated value:  -2
Extrapolated value:  11
Extrapolated value:  9
Extrapolated value:  -4
Extrapolated value:  12
Extrapolated value:  -4
Extrapolated value:  7
Extrapolated value:  -2
Extrapolated value:  4
Extrapolated value:  4
Extrapolated value:  0
Extrapolated value:  1
Extrapolated value:  11
Extrapolated value:  8
Extrapolated value:  3
Extrapolated value:  12
Extrapolated value:  14
Extrapolated value:  10
Extrapolated value:  11
Extrapolated value:  5
Extrapolated value:  -5
Extrapolated value:  6
Extrapolated value:  0
Extrapolated value:  -2
Extrapolated value:  9
Extrapolated value:  3
Extrapolated value:  0
Answer is:  919
*/