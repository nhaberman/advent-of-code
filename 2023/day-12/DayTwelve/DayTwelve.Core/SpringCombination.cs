using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTwelve.Core
{
    public class SpringCombination : IEquatable<SpringCombination>
    {
        private List<SpringCondition> Springs { get; }

        public int SpringCount => Springs.Count;

        public SpringCombination()
        {
            Springs = new();
        }

        public SpringCombination(SpringCombination springCombination)
        {
            Springs = new(springCombination.Springs);
        }

        public void AddSpring(SpringCondition spring)
        {
            Springs.Add(spring);
        }

        public void AddSpring(char springChar)
        {
            AddSpring(springChar switch {
                '.' => SpringCondition.Operational,
                '#' => SpringCondition.Damaged,
                '?' => SpringCondition.Unknown,
                _ => throw new InvalidOperationException($"Invalid spring character detected:  {springChar}"),
            });
        }

        public void AddSprings(IEnumerable<SpringCondition> springs)
        {
            Springs.AddRange(springs);
        }

        public void AddSprings(SpringCondition springToAdd, int numberOfSpringsToAdd)
        {
            AddSprings(Enumerable.Repeat(springToAdd, numberOfSpringsToAdd));
        }

        public bool TestSprings(SpringCombination springs)
        {
            if (springs.SpringCount != SpringCount)
            {
                return false;
            }

            // test each spring in the list
            for (int i = 0; i < springs.SpringCount; i++)
            {
                if (springs.Springs[i] != Springs[i] && springs.Springs[i] != SpringCondition.Unknown)
                {
                    return false;
                }
            }

            // if all springs passed, then return true
            return true;
        }

        public SpringCondition GetSpringAtIndex(int index)
        {
            return Springs[index];
        }

        public override string ToString()
        {
            StringBuilder result = new();

            Springs.ForEach(spring => result.Append(spring switch
            {
                SpringCondition.Unknown => '?',
                SpringCondition.Operational => '.',
                SpringCondition.Damaged => '#',
                _ => throw new NotImplementedException(),
            }));

            return result.ToString();
        }

        #region IEquatable support
        public bool Equals(SpringCombination? other)
        {
            if (other is null)
            {
                return false;
            }
            else if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            else if (GetType() != other.GetType())
            {
                return false;
            }
            else
            {
                return Springs.SequenceEqual(other.Springs);
            }
        }

        public static bool operator ==(SpringCombination? left, SpringCombination? right)
        {
            if (left is null && right is null)
            {
                return true;
            }
            else if (left is not null && right is not null)
            {
                return left.Equals(right);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(SpringCombination? left, SpringCombination? right) => !(left == right);

        public override bool Equals(object? obj) => Equals(obj as SpringCombination);

        public override int GetHashCode() => Springs.Sum(spring => (int)spring).GetHashCode();
        #endregion
    }
}