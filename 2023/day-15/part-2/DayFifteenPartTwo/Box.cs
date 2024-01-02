using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DayFifteenPartTwo
{
    public class Box
    {
        public byte Number { get; }

        public List<Lens> Lenses { get; } = new();

        public Box(int number)
        {
            Number = (byte)number;
        }

        public void RemoveLens(string lensLabel)
        {
            if (Lenses.Any(lens => lens.Label == lensLabel))
            {
                int lensIndex = Lenses.FindIndex(lens => lens.Label == lensLabel);

                // remove the lens from the box
                Lenses.RemoveAt(lensIndex);
            }
        }

        public void AddLens(Lens lensToAdd)
        {
            if (Lenses.Any(lens => lens.Label == lensToAdd.Label))
            {
                // replace the existing lens with the new lens
                int lensIndex = Lenses.FindIndex(lens => lens.Label == lensToAdd.Label);
                Lenses.RemoveAt(lensIndex);
                Lenses.Insert(lensIndex, lensToAdd);
            }
            else
            {
                // add the lens to the end of the list
                Lenses.Add(lensToAdd);
            }
        }

        public int GetFocusingPower()
        {
            int result = 0;

            for (int i = 0; i < Lenses.Count; i++)
            {
                result += (Number + 1) * (i + 1) * Lenses[i].FocalLength;
            }

            return result;
        }

        public override string ToString() => $"{Number} ({Lenses.Count} lenses)";
    }
}