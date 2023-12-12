// See https://aka.ms/new-console-template for more information
using DayFivePartOne;
using System.Text.RegularExpressions;

Console.WriteLine("Running solution for Day 5, part 1...");

var seeds = new List<long>();
Map seedToSoilMap,
    soilToFertilizerMap,
    fertilizerToWaterMap,
    waterToLightMap,
    lightToTemperatureMap,
    temperatureToHumididyMap,
    humidityToLocationMap;
var seedLocations = new Dictionary<long, long>();

// get file contents
var almanac = File.ReadAllLines("../../input-5.txt");

// get the seeds
var seedLine = almanac.First(line => line.StartsWith("seeds: "));
Regex seedRegex = new(@"\d+");
MatchCollection matches = seedRegex.Matches(seedLine);
foreach (Match match in matches)
{
    seeds.Add(long.Parse(match.Value));
}

// get each map
seedToSoilMap = Map.CreateMap(almanac, "seed-to-soil map");
soilToFertilizerMap = Map.CreateMap(almanac, "soil-to-fertilizer map");
fertilizerToWaterMap = Map.CreateMap(almanac, "fertilizer-to-water map");
waterToLightMap = Map.CreateMap(almanac, "water-to-light map");
lightToTemperatureMap = Map.CreateMap(almanac, "light-to-temperature map");
temperatureToHumididyMap = Map.CreateMap(almanac, "temperature-to-humidity map");
humidityToLocationMap = Map.CreateMap(almanac, "humidity-to-location map");

// run each seed through the map
foreach (var seed in seeds)
{
    var soil = seedToSoilMap.GetMapping(seed);
    var fertilizer = soilToFertilizerMap.GetMapping(soil);
    var water = fertilizerToWaterMap.GetMapping(fertilizer);
    var light = waterToLightMap.GetMapping(water);
    var temperature = lightToTemperatureMap.GetMapping(light);
    var humidity = temperatureToHumididyMap.GetMapping(temperature);
    var location = humidityToLocationMap.GetMapping(humidity);

    Console.WriteLine($"Location for seed {seed} is {location}.");
    seedLocations.Add(seed, location);
}

// determine which seed's location is the lowest
var answer = seedLocations.Values.Min();

Console.WriteLine($"Answer is:  {answer}");


/*
Location for seed 1848591090 is 3678920559.
Location for seed 462385043 is 1372355363.
Location for seed 2611025720 is 3006006012.
Location for seed 154883670 is 910845529.
Location for seed 1508373603 is 4103643320.
Location for seed 11536371 is 2505871512.
Location for seed 3692308424 is 3988139085.
Location for seed 16905163 is 2511240304.
Location for seed 1203540561 is 3357962615.
Location for seed 280364121 is 1712054209.
Location for seed 3755585679 is 4018811971.
Location for seed 337861951 is 3972216376.
Location for seed 93589727 is 3879699406.
Location for seed 738327409 is 1950709931.
Location for seed 3421539474 is 1198579958.
Location for seed 257441906 is 1689131994.
Location for seed 3119409201 is 1044732983.
Location for seed 243224070 is 1674914158.
Location for seed 50985980 is 3479205958.
Location for seed 7961058 is 2502296199.
Answer is:  910845529
*/


