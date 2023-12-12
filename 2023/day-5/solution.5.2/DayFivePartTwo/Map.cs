using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DayFivePartTwo
{
    public class Map
    {
        private List<MapComponent> MapComponents { get; }

        public Map(MapComponent[] mapComponents)
        {
            MapComponents = new(mapComponents);
        }

        public long GetMapping(long source)
        {
            foreach (var mapComponent in MapComponents)
            {
                if (mapComponent.IsInRange(source))
                {
                    return mapComponent.MapResult(source);
                }
            }

            // if no map component describes the source, just return the source
            return source;
        }

        public static Map CreateMap(string[] almanac, string mapName)
        {
            List<MapComponent> mapComponents = new();

            // find the map lines in the almanac
            var mapLines = almanac
                .SkipWhile(line => !line.StartsWith(mapName))   // find the map name
                .Skip(1)    // skip the map name line
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line));   // continue until no more components

            Regex regex = new(@"(?<dest>\d+) (?<source>\d+) (?<length>\d+)");
            foreach (var line in mapLines)
            {
                Match match = regex.Match(line);

                if (match.Success)
                {
                    MapComponent mapComponent = new(
                        long.Parse(match.Groups["dest"].Value),
                        long.Parse(match.Groups["source"].Value),
                        long.Parse(match.Groups["length"].Value));

                    mapComponents.Add(mapComponent);
                }
            }

            // return the map
            return new Map(mapComponents.ToArray());
        }
    }
}