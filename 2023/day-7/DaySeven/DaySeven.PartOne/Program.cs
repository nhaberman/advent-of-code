// See https://aka.ms/new-console-template for more information
using DaySeven.Core;
using System.Text.RegularExpressions;

Console.WriteLine("Running solution for Day 7, part 1...");

// get file contents
//var input = File.ReadAllLines("../../input-7-demo.txt");
var input = File.ReadAllLines("../../input-7.txt");

// collect the hands
var hands = new List<Hand>();
Regex regex = new(@"(?<hand>\w{5}) (?<bid>\d+)");
foreach (var line in input)
{
    Match match = regex.Match(line);
    if (match.Success)
    {
        string cards = match.Groups["hand"].Value;
        int bid = int.Parse(match.Groups["bid"].Value);
        Hand hand = new(cards, bid);
        hands.Add(hand);
    }
}

Console.WriteLine($"Found {hands.Count} hands.");

// order the hands
var orderedHands = hands.OrderBy(hand => hand).ToList();

// calculate the winnings
int winnings = 0;
for (int i = 0; i < orderedHands.Count; i++)
{
    winnings += orderedHands[i].Bid * (i + 1);
}

Console.WriteLine($"Answer is:  {winnings}");

/*
Running solution for Day 7, part 1...
Found 1000 hands.
Answer is:  252052080
*/