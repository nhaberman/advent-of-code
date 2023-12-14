using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayNine.Core
{
    public class DerivedSequenceCollection
    {
        List<Sequence> DerivedSequences { get; }

        public bool IsFinalSequenceZeros => DerivedSequences.Last().IsZeroSequence;

        public DerivedSequenceCollection(Sequence baseSequence)
        {
            DerivedSequences = new()
            {
                baseSequence
            };
        }

        public void CalculateDerivedSequences()
        {
            while (!IsFinalSequenceZeros)
            {
                DerivedSequences.Add(new(DerivedSequences.Last()));
            }
        }

        public int ExtrapolateSequences()
        {
            // loop through the sequences in reverse order
            for (int i = DerivedSequences.Count - 1; i >= 0; i--)
            {
                // if a zero sequence, just add another zero
                if (DerivedSequences[i].IsZeroSequence)
                {
                    DerivedSequences[i].Values.Add(0);
                }
                else
                {
                    int extrapolatedValue = DerivedSequences[i].Values.Last() + DerivedSequences[i + 1].Values.Last();
                    DerivedSequences[i].Values.Add(extrapolatedValue);
                }
            }
            
            // return the last added value
            int finalExtrapolatedValue = DerivedSequences[0].Values.Last();
            Console.WriteLine($"Extrapolated value:  {finalExtrapolatedValue}");
            return finalExtrapolatedValue;
        }

        public int ExtrapolateSequencesBackwards()
        {
            // loop through the sequences in reverse order
            for (int i = DerivedSequences.Count - 1; i >= 0; i--)
            {
                // if a zero sequence, just add another zero
                if (DerivedSequences[i].IsZeroSequence)
                {
                    DerivedSequences[i].Values.Insert(0, 0);
                }
                else
                {
                    int extrapolatedValue = DerivedSequences[i].Values.First() - DerivedSequences[i + 1].Values.First();
                    DerivedSequences[i].Values.Insert(0, extrapolatedValue);
                }
            }
            
            // return the last added value
            int finalExtrapolatedValue = DerivedSequences[0].Values.First();
            Console.WriteLine($"Extrapolated value:  {finalExtrapolatedValue}");
            return finalExtrapolatedValue;
        }
    }
}