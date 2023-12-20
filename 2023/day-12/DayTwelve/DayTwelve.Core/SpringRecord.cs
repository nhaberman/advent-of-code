using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTwelve.Core
{
    public class SpringRecord
    {
        public SpringCombination Springs { get; }

        public List<int> DamagedSpringGroups { get; }

        public int MinimumSpringSize => DamagedSpringGroups.Sum() + DamagedSpringGroups.Count() - 1;

        public int TotalValidArrangements { get; set; }

        public SpringRecord(string input)
        {
            Springs = new();
            DamagedSpringGroups = new();
            InterpretInput(input);
        }

        private void InterpretInput(string input)
        {
            // split the line into the springs and damaged spring groups
            var inputs = input.Split(' ');
            string springs = inputs[0];
            string groups = inputs[1];

            foreach (char springChar in springs)
            {
                Springs.AddSpring(springChar);
            }

            foreach (var groupCount in groups.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                DamagedSpringGroups.Add(int.Parse(groupCount));
            }
        }

        public static string UnfoldSpringRecordInput(string input)
        {
            StringBuilder result = new();

            // split the line into the springs and damaged spring groups
            var inputs = input.Split(' ');
            string springs = inputs[0];
            string groups = inputs[1];

            // add five copies of the string conditions
            for (int i = 0; i < 5; i++)
            {
                result.Append(springs + "?");
            }

            // add the middle space
            result.Append(" ");

            // add five copies of the string groups
            for (int i = 0; i < 5; i++)
            {
                result.Append(groups + ",");
            }

            return result.ToString();
        }

        public void CalculatePossibleArangements()
        {
            // find all the possible spring arangements based on the group numbers alone
            List<SpringCombination> possibleSpringArrangements = GetAllPossibleSpringCombinations();
            
            // examine each possible arrangement to see if it sastisfies the known Springs
            foreach (SpringCombination springList in possibleSpringArrangements)
            {
                // count if a valid arrangement
                if (springList.TestSprings(Springs))
                {
                    TotalValidArrangements++;
                }
            }

            //Console.WriteLine($"{Springs} - possible arrangements:  {TotalValidArrangements}");
        }

        private List<SpringCombination> GetAllPossibleSpringCombinations(SpringCombination? baseSpringList = null, int groupsInBaseList = 0)
        {
            var results = new List<SpringCombination>();

            // if no spring list supplied, start with an empty list
            baseSpringList ??= new();

            // find all possibilties to shift the next spring group
            var remainingSpringGroups = DamagedSpringGroups.Skip(groupsInBaseList + 1);
            int possibleStartingSlots = Springs.SpringCount
                - baseSpringList.SpringCount
                - remainingSpringGroups.Sum()
                - remainingSpringGroups.Count()
                - DamagedSpringGroups[groupsInBaseList];

            // for each possible starting slot, build a base list and repeat if necessary
            for (int i = 0; i <= possibleStartingSlots; i++)
            {
                SpringCombination newCombination = new(baseSpringList);

                // append a number of operational springs
                newCombination.AddSprings(SpringCondition.Operational, i);
                
                // append the damaged springs for this group
                newCombination.AddSprings(SpringCondition.Damaged, DamagedSpringGroups[groupsInBaseList]);

                // if there are no more spring groups, pad any remainder with operational springs
                if (groupsInBaseList + 1== DamagedSpringGroups.Count)
                {
                    newCombination.AddSprings(SpringCondition.Operational, Springs.SpringCount - newCombination.SpringCount);

                    // add the combination to the results
                    results.Add(newCombination);
                }
                // otherwise add a spacer operational spring and repeat
                else
                {
                    // add a single operational spring
                    newCombination.AddSpring(SpringCondition.Operational);

                    results.AddRange(GetAllPossibleSpringCombinations(newCombination, groupsInBaseList + 1));
                }
            }

            return results;
        }

        public void CalculatePossibleArangements2()
        {
            // find all the possible spring arangements based on the group numbers alone
            List<SpringCombination> possibleSpringArrangements = GetAllPossibleSpringCombinations2();
            
            // examine each possible arrangement to see if it sastisfies the known Springs
            foreach (SpringCombination springList in possibleSpringArrangements)
            {
                // count if a valid arrangement
                if (springList.TestSprings(Springs))
                {
                    TotalValidArrangements++;
                }
            }

            Console.WriteLine($"{Springs} - possible arrangements:  {TotalValidArrangements}");
        }

        private List<SpringCombination> GetAllPossibleSpringCombinations2(SpringCombination? baseSpringList = null, int groupsInBaseList = 0)
        {
            var results = new List<SpringCombination>();

            // if no spring list supplied, start with an empty list
            baseSpringList ??= new();

            // find all possibilties to shift the next spring group
            var remainingSpringGroups = DamagedSpringGroups.Skip(groupsInBaseList + 1);
            int possibleStartingSlots = Springs.SpringCount
                - baseSpringList.SpringCount
                - remainingSpringGroups.Sum()
                - remainingSpringGroups.Count()
                - DamagedSpringGroups[groupsInBaseList];

            // for each possible starting slot, build a base list and repeat if necessary
            for (int i = 0; i <= possibleStartingSlots; i++)
            {
                SpringCombination newCombination = new(baseSpringList);

                // append a number of operational springs
                newCombination.AddSprings(SpringCondition.Operational, i);

                // check if there are any operational springs in the next group's space
                bool isValidPosition = true;
                for (int j = 0; j < DamagedSpringGroups[groupsInBaseList]; j++)
                {
                    // check if there should be damaged springs at this position
                    if (Springs.GetSpringAtIndex(newCombination.SpringCount + j) == SpringCondition.Operational)
                    {
                        isValidPosition = false;
                        break;
                    }
                }

                if (!isValidPosition)
                {
                    // this is not a valid spring position, do nothing
                    continue;
                }
                
                // append the damaged springs for this group
                newCombination.AddSprings(SpringCondition.Damaged, DamagedSpringGroups[groupsInBaseList]);

                // if there are no more spring groups, pad any remainder with operational springs
                if (groupsInBaseList + 1== DamagedSpringGroups.Count)
                {
                    newCombination.AddSprings(SpringCondition.Operational, Springs.SpringCount - newCombination.SpringCount);

                    // add the combination to the results
                    results.Add(newCombination);
                }
                // otherwise add a spacer operational spring and repeat
                else
                {
                    // add a single operational spring
                    newCombination.AddSpring(SpringCondition.Operational);

                    results.AddRange(GetAllPossibleSpringCombinations(newCombination, groupsInBaseList + 1));
                }
            }

            return results;
        }
    }
}