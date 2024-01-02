using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayFifteenPartTwo
{
    public class Step
    {
        public string Label { get; }

        public char Operation { get; }

        public int? FocalLength { get; }

        public Step(string initializationStep)
        {
            Label = string.Concat(initializationStep.TakeWhile(character => character != '-' && character != '='));
            Operation = initializationStep.Contains('-') ? '-' : '=';
            FocalLength = Operation == '-' ? null : 
                int.Parse(string.Concat(initializationStep.SkipWhile(character => character != '-' && character != '=').Skip(1)));
        }

        public byte GetBoxNumber()
        {
            // run the HASH algorithm on the step label
            return Hash.RunHashAlgorithmOnString(Label);
        }

        public override string ToString() => $"{Label}{Operation}{FocalLength?.ToString() ?? string.Empty}";
    }
}