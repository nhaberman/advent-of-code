using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayNine.Core
{
    public class Sequence
    {
        public List<int> Values { get; }

        public bool IsZeroSequence => Values.All(value => value == 0);

        public Sequence(IEnumerable<int> values)
        {
            Values = new(values);
        }

        public Sequence(Sequence baseSequence)
        {
            Values = new(GenerateDifferences(baseSequence.Values));
        }

        private static List<int> GenerateDifferences(List<int> values)
        {
            List<int> results = new();

            for (int i = 1; i < values.Count; i++)
            {
                int difference = values[i] - values[i - 1];
                results.Add(difference);
            }

            return results;
        }
    }
}