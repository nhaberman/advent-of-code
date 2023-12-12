// See https://aka.ms/new-console-template for more information
using DayFivePartTwo;
using System.Text.RegularExpressions;

Console.WriteLine("Running solution for Day 5, part 2...");

var seedBuckets = new List<(long start, long length)>();
Map seedToSoilMap,
    soilToFertilizerMap,
    fertilizerToWaterMap,
    waterToLightMap,
    lightToTemperatureMap,
    temperatureToHumididyMap,
    humidityToLocationMap;
var seedLocations = new List<long>();

// get file contents
var almanac = File.ReadAllLines("../../input-5.txt");

// get the seeds
var seedLine = almanac.First(line => line.StartsWith("seeds: "));
Regex seedRegex = new(@"(?<start>\d+) (?<length>\d+)");
MatchCollection matches = seedRegex.Matches(seedLine);
foreach (Match match in matches)
{
    long start = long.Parse(match.Groups["start"].Value);
    long length = long.Parse(match.Groups["length"].Value);

    seedBuckets.Add((start, length));
    Console.WriteLine($"Added {length:N0} seeds, starting with {start:N0}.");
}
Console.WriteLine($"Found a total of {seedBuckets.Select(item => item.length).Sum():N0} seeds in {seedBuckets.Count} buckets.");

// get each map
seedToSoilMap = Map.CreateMap(almanac, "seed-to-soil map");
soilToFertilizerMap = Map.CreateMap(almanac, "soil-to-fertilizer map");
fertilizerToWaterMap = Map.CreateMap(almanac, "fertilizer-to-water map");
waterToLightMap = Map.CreateMap(almanac, "water-to-light map");
lightToTemperatureMap = Map.CreateMap(almanac, "light-to-temperature map");
temperatureToHumididyMap = Map.CreateMap(almanac, "temperature-to-humidity map");
humidityToLocationMap = Map.CreateMap(almanac, "humidity-to-location map");

// calculate the lowest location for each seed bucket
for (int i = 0; i < seedBuckets.Count; i++)
{
    long lowestLocation = long.MaxValue;
    var seedBucketGroup = seedBuckets[i];

    Console.WriteLine($"Examining {seedBucketGroup.length:N0} seeds starting with seed {seedBucketGroup.start:N0}...");

    for (long j = 0; j < seedBucketGroup.length; j++)
    {
        long seed = seedBucketGroup.start + j;

        // periodically log progress
        if (seedBucketGroup.length > 100 && seed % (seedBucketGroup.length / 100) == 0)
        {
            Console.Write("*");
        }

        // calculate the position for each seed
        var soil = seedToSoilMap.GetMapping(seed);
        var fertilizer = soilToFertilizerMap.GetMapping(soil);
        var water = fertilizerToWaterMap.GetMapping(fertilizer);
        var light = waterToLightMap.GetMapping(water);
        var temperature = lightToTemperatureMap.GetMapping(light);
        var humidity = temperatureToHumididyMap.GetMapping(temperature);
        var location = humidityToLocationMap.GetMapping(humidity);

        // save the location if it is the lowest
        lowestLocation = Math.Min(lowestLocation, location);
    }
    
    // if necessary, log that the loop is complete
    if (seedBucketGroup.length > 100)
    {
        Console.WriteLine("+");
    }

    // save the lowest location for this group
    Console.WriteLine($"Lowest seed location was {lowestLocation:N0}.");
    seedLocations.Add(lowestLocation);
}

// determine which seed's location is the lowest
var answer = seedLocations.Min();

Console.WriteLine($"Answer is:  {answer}");

/*
Running solution for Day 5, part 2...
Added 462,385,043 seeds, starting with 1,848,591,090.
Added 154,883,670 seeds, starting with 2,611,025,720.
Added 11,536,371 seeds, starting with 1,508,373,603.
Added 16,905,163 seeds, starting with 3,692,308,424.
Added 280,364,121 seeds, starting with 1,203,540,561.
Added 337,861,951 seeds, starting with 3,755,585,679.
Added 738,327,409 seeds, starting with 93,589,727.
Added 257,441,906 seeds, starting with 3,421,539,474.
Added 243,224,070 seeds, starting with 3,119,409,201.
Added 7,961,058 seeds, starting with 50,985,980.
Found a total of 2,510,890,762 seeds in 10 buckets.
Examining 462,385,043 seeds starting with seed 1,848,591,090...
****************************************************************************************************+
Lowest seed location was 95,461,669.
Examining 154,883,670 seeds starting with seed 2,611,025,720...
****************************************************************************************************+
Lowest seed location was 653,925,960.
Examining 11,536,371 seeds starting with seed 1,508,373,603...
****************************************************************************************************+
Lowest seed location was 562,057,898.
Examining 16,905,163 seeds starting with seed 3,692,308,424...
****************************************************************************************************+
Lowest seed location was 3,520,380,446.
Examining 280,364,121 seeds starting with seed 1,203,540,561...
****************************************************************************************************+
Lowest seed location was 292,211,340.
Examining 337,861,951 seeds starting with seed 3,755,585,679...
****************************************************************************************************+
Lowest seed location was 243,583,567.
Examining 738,327,409 seeds starting with seed 93,589,727...
****************************************************************************************************+
Lowest seed location was 368,703,957.
Examining 257,441,906 seeds starting with seed 3,421,539,474...
****************************************************************************************************+
Lowest seed location was 791,316,226.
Examining 243,224,070 seeds starting with seed 3,119,409,201...
****************************************************************************************************+
Lowest seed location was 77,435,348.
Examining 7,961,058 seeds starting with seed 50,985,980...
****************************************************************************************************+
Lowest seed location was 1,526,666,011.
Answer is:  77435348
*/