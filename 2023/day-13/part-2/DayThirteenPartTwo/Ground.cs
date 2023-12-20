using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayThirteenPartTwo
{
    public class Ground
    {
        public char[,] Pattern { get; set; }

        public bool? IsRowReflection { get; set; }

        public bool? IsColumnReflection => IsRowReflection is null ? null : !IsRowReflection;

        /// <summary>
        /// Reflection is between row at index <see cref="ReflectionIndex"/> and row at index <see cref="ReflectionIndex"/> + 1;
        /// </summary>
        public int? ReflectionIndex { get; set; }

        public int? ReflectionLine => ReflectionIndex is null ? null : ReflectionIndex + 1;

        public Ground(List<string> input)
        {
            Pattern = new char[input.Count, input[0].Length];
            ParseInput(input);

            IsRowReflection = null;
            ReflectionIndex = null;
        }

        private void ParseInput(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                var lineChars = input[i].ToCharArray();
                
                for (int j = 0; j < lineChars.Length; j++)
                {
                    Pattern[i,j] = lineChars[j];
                }
            }
        }

        public override string ToString()
        {
            StringBuilder result = new();

            for (int i = 0; i < Pattern.GetLength(0); i++)
            {
                for (int j = 0; j < Pattern.GetLength(1); j++)
                {
                    result.Append(Pattern[i, j]);
                }
                result.AppendLine();
            }

            return result.ToString();
        }

        public bool CalculateReflection()
        {
            // look through every row to see if it is a possible reflection
            for (int i = 0; i < Pattern.GetLength(0); i++)
            {
                if (TestIfRowReflection(i))
                {
                    IsRowReflection = true;
                    ReflectionIndex = i;
                    return true;
                }
            }

            // look through every column to see if it is a possible reflection
            for (int j = 0; j < Pattern.GetLength(1); j++)
            {
                if (TestIfColumnReflection(j))
                {
                    IsRowReflection = false;
                    ReflectionIndex = j;
                    return true;
                }
            }

            // if no reflection was calculated, return false
            IsRowReflection = null;
            ReflectionIndex = null;
            return false;
        }

        private bool TestIfRowReflection(int rowIndex)
        {
            int distanceFromReflection = 0;

            // if not in the middle of the pattern, return false
            if (rowIndex < 0 || rowIndex + 2 > Pattern.GetLength(0))
            {
                return false;
            }

            while (rowIndex - distanceFromReflection >= 0
                && rowIndex + 1 + distanceFromReflection < Pattern.GetLength(0))
            {
                // check if the rows match
                for (int i = 0; i < Pattern.GetLength(1); i++)
                {
                    if (Pattern[rowIndex - distanceFromReflection, i] != Pattern[rowIndex + 1 + distanceFromReflection, i])
                    {
                        return false;
                    }
                }

                // increase the distance and repeat
                distanceFromReflection++;
            }

            return true;
        }

        private bool TestIfColumnReflection(int columnIndex)
        {
            int distanceFromReflection = 0;

            // if not in the middle of the pattern, return false
            if (columnIndex < 0 || columnIndex + 2 > Pattern.GetLength(1))
            {
                return false;
            }

            while (columnIndex - distanceFromReflection >= 0
                && columnIndex + 1 + distanceFromReflection < Pattern.GetLength(1))
            {
                // check if the columns match
                for (int i = 0; i < Pattern.GetLength(0); i++)
                {
                    if (Pattern[i, columnIndex - distanceFromReflection] != Pattern[i, columnIndex + 1 + distanceFromReflection])
                    {
                        return false;
                    }
                }

                // increase the distance and repeat
                distanceFromReflection++;
            }

            return true;
        }

        public void FixSmudge()
        {
            // save the original reflection and then reset the reflection
            bool isOriginalRowReflection = IsRowReflection ?? throw new Exception("Reflection not yet calculated.");
            int originalReflectionIndex = ReflectionIndex ?? throw new Exception("Reflection not yet calculated.");
            IsRowReflection = null;
            ReflectionIndex = null;

            int counter = 0;
            Console.WriteLine($"Examining pattern...");

            // loop through each position and "fix the smudge" by flipping '.'s to '#'s (and vice versa)
            // after each possible fix, test the pattern for a reflection
            for (int rowIndex = 0; rowIndex < Pattern.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Pattern.GetLength(1); columnIndex++)
                {
                    counter++;

                    // flip the character in the current position
                    char currentGround = Pattern[rowIndex, columnIndex];
                    char flippedGround = currentGround switch {
                        '.' => '#',
                        '#' => '.',
                        _ => throw new InvalidDataException(),
                    };
                    Pattern[rowIndex, columnIndex] = flippedGround;

                    // try to calculate a reflection
                    if (CalculateNewReflection(isOriginalRowReflection, originalReflectionIndex))
                    {
                        Console.WriteLine($"  Fixed the smudge at row {rowIndex + 1}, column {columnIndex + 1} after {counter} attempts.");
                        Console.WriteLine($"    The new reflection is at {(IsRowReflection ?? false ? "row" : "column")} {ReflectionLine}.");
                        return;
                    }

                    // if no reflection, restore the original character and continue
                    Pattern[rowIndex, columnIndex] = currentGround;
                }
            }

            // (should never reach this code)
            Console.WriteLine("No smudge was found!");
            IsRowReflection = isOriginalRowReflection;
            ReflectionIndex = originalReflectionIndex;
        }

        public bool CalculateNewReflection(bool isOriginalRowReflection, int originalReflectionIndex)
        {
            // look through every row to see if it is a possible reflection
            for (int i = 0; i < Pattern.GetLength(0); i++)
            {
                if (TestIfRowReflection(i))
                {
                    // only save a new reflection
                    if (!isOriginalRowReflection || i != originalReflectionIndex)
                    {
                        IsRowReflection = true;
                        ReflectionIndex = i;
                        return true;
                    }
                }
            }

            // look through every column to see if it is a possible reflection
            for (int j = 0; j < Pattern.GetLength(1); j++)
            {
                if (TestIfColumnReflection(j))
                {
                    // only save a new reflection
                    if (isOriginalRowReflection || j != originalReflectionIndex)
                    {
                        IsRowReflection = false;
                        ReflectionIndex = j;
                        return true;
                    }
                }
            }

            // if no reflection was calculated, return false
            IsRowReflection = null;
            ReflectionIndex = null;
            return false;
        }
    }
}