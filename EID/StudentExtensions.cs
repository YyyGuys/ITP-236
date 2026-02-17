using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EID
{
    public static class StudentExtensions
    {
        /// Checks if a number is odd by using the modulus operator to determine if there is a remainder when divided by 2. 
        /// If there is a remainder, the number is odd, and the method returns true; otherwise, it returns false.
        /// Submitted by David Jenerette
        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }
        /// <summary>
        /// Determines whether the string contains only numeric characters (0-9).
        /// Submitted by Jonathan Jellen
        /// </summary>
        /// <param name="value">The string to evaluate. Can be null or empty.</param>
        /// <returns><see langword="true"/> if the string contains only numeric digits; otherwise, <see langword="false"/>.
        /// Returns <see langword="false"/> if <paramref name="value"/> is null or empty.</returns>
        public static bool IsNumeric(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether the string contains only alphabetic characters (A-Z, a-z).
        /// Submitted by Jonathan Jellen
        /// </summary>
        /// <param name="value">The string to evaluate. Can be null or empty.</param>
        /// <returns><see langword="true"/> if the string contains only alphabetic letters; otherwise, <see langword="false"/>.
        /// Returns <see langword="false"/> if <paramref name="value"/> is null or empty.</returns>
        public static bool IsAlpha(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(char.IsLetter);
        }
        /// <summary>
        /// Determines whether an integer is even.
        /// Submitted by Joshua Greene
        /// </summary>
        /// <param name="number">The integer to check.</param>
        /// <returns>True if even; otherwise false.</returns>
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
    }
}
