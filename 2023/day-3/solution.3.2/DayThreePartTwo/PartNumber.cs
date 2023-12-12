using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayThreePartTwo
{
    public class PartNumber
    {
        public int Line { get; }

        public int Index { get; }

        public int Length { get; }

        public int Value { get; }

        public PartNumber(int value, int line, int index, int length)
        {
            Value = value;
            Line = line;
            Index = index;
            Length = length;
        }

        public override string ToString() => $"{Value} ({Line},{Index})";
    }
}