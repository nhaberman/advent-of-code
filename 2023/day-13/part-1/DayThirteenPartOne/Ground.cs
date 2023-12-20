using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayThirteenPartOne
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

        public void CalculateReflection()
        {
            // look through every row to see if it is a possible reflection
            for (int i = 0; i < Pattern.GetLength(0); i++)
            {
                if (TestIfRowReflection(i))
                {
                    IsRowReflection = true;
                    ReflectionIndex = i;
                    return;
                }
            }

            // look through every column to see if it is a possible reflection
            for (int j = 0; j < Pattern.GetLength(1); j++)
            {
                if (TestIfColumnReflection(j))
                {
                    IsRowReflection = false;
                    ReflectionIndex = j;
                    return;
                }
            }
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
    }
}