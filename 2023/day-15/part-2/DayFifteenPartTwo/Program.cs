// See https://aka.ms/new-console-template for more information
using DayFifteenPartTwo;

Console.WriteLine("Running solution for Day 15, part 2...");

// get file contents
//var input = File.ReadAllLines("../../input-15-demo.txt");
var input = File.ReadAllLines("../../input-15.txt");

// create the boxes
List<Box> boxes = new();
for (int i = 0; i < 256; i++)
{
    boxes.Add(new(i));
}
Console.WriteLine($"Created {boxes.Count} boxes.");

// read the steps
List<Step> steps = new();
var stepStrings = string.Join(string.Empty, input).Split(',');
foreach (string stepString in stepStrings)
{
    steps.Add(new(stepString));
}
Console.WriteLine($"Found {steps.Count} steps in the Initialization Sequence.");

// perform the steps in the sequence
foreach (Step step in steps)
{
    // determine the box for the step
    var boxNumber = step.GetBoxNumber();
    Box boxInStep = boxes.First(box => box.Number == boxNumber);

    // add or remove the lens based on the operation
    if (step.Operation == '=')
    {
        Lens lensToAdd = new(step.Label, step.FocalLength ?? -1);
        boxInStep.AddLens(lensToAdd);
    }
    else if (step.Operation == '-')
    {
        boxInStep.RemoveLens(step.Label);
    }
}

// calculate the answer
int answer = 0;

foreach (Box box in boxes)
{
    answer += box.GetFocusingPower();
}

Console.WriteLine($"Answer is:  {answer}");

/*
Running solution for Day 15, part 2...
Created 256 boxes.
Found 4000 steps in the Initialization Sequence.
Answer is:  267372
*/

/*
267372 -> correct!
*/