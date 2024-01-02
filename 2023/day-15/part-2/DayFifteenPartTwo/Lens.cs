using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayFifteenPartTwo
{
    public class Lens
    {
        public string Label { get; }

        public int FocalLength { get; }

        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;

            // verify the focal length
            if (FocalLength < 1 || FocalLength > 9)
            {
                throw new ArgumentOutOfRangeException($"{FocalLength} is not a valid focal length");
            }
        }

        public override string ToString() => $"[{Label} {FocalLength}]";
    }
}