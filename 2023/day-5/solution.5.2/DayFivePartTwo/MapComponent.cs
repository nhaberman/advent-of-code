using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayFivePartTwo
{
    public readonly struct MapComponent
    {
        public long DestinationRangeStart { get; }

        public long SourceRangeStart { get; }

        public long SourceRangeEnd => SourceRangeStart + RangeLength;

        public long RangeLength { get; }

        public MapComponent(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }

        public bool IsInRange(long index) => index >= SourceRangeStart && index < SourceRangeEnd;

        public long MapResult(long index) 
        {
            long positionInMap = index - SourceRangeStart;
            return DestinationRangeStart + positionInMap;
        }
    }
}