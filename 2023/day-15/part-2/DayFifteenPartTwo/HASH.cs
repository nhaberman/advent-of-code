using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayFifteenPartTwo
{
    public static class Hash
    {
        public static byte RunHashAlgorithmOnString(string input)
        {
            int result = 0;

            foreach (char inputChar in input.ToCharArray())
            {
                RunHashAlgorithm(inputChar, ref result);
            }

            return (byte)result;
        }

        public static void RunHashAlgorithm(char input, ref int currentValue)
        {
            // get the ASCII code for the character
            byte charCode = (byte)input;

            // increase the current value by the ASCII code value
            currentValue += charCode;

            // multiply the current value by 17
            currentValue *= 17;

            // divide the current value by 256 and set the remainder as the current value
            currentValue %= 256;
        }
    }
}