using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayThreePartTwo
{
    public class Gear
    {
        public int Line { get; }

        public int Index { get; }

        public Gear(int line, int index)
        {
            Line = line;
            Index = index;
        }
        
        public bool IsGear(PartNumber part1, PartNumber part2)
        {
            return IsPartNumberAdjacent(part1) && IsPartNumberAdjacent(part2);
        }

        private bool IsPartNumberAdjacent(PartNumber partNumber) 
        {
            // confirm the part number's line is adjacent to the gear's line
            if (partNumber.Line < Line - 1 || partNumber.Line > Line + 1)
            {
                return false;
            }

            // check if the part number's index is adjacent to the gear
            for (int i = 0; i <= partNumber.Length + 1; i++)
            {
                if (partNumber.Index - 1 + i == Index)
                {
                    return true;
                }
            }

            // if no adjacent index was found, return false
            return false;
        }

        public override string ToString() => $"{{{Line},{Index}}}";
    }
}