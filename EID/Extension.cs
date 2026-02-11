using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EID
{
    public static class Extension
    {
        /// <summary>
        /// Determines whether the first character of the specified string is an uppercase letter.
        /// </summary>
        /// <param name="value">The string to evaluate. Can be null or empty.</param>
        /// <returns><see langword="true"/> if the first character of <paramref name="value"/> is an uppercase letter; otherwise,
        /// <see langword="false"/>. Returns <see langword="false"/> if <paramref name="value"/> is null or empty.</returns>
        public static bool IsCapitalized(this string value)
        {
            return !string.IsNullOrEmpty(value) && char.IsUpper(value[0]);
        }
        /// <summary>
        /// Returns a substring containing the leftmost characters of the specified string.
        /// </summary>
        /// <param name="value">The string from which to extract the substring. Can be null or empty.</param>
        /// <param name="length">The number of characters to return from the start of the string. Must be zero or greater.</param>
        /// <returns>A string containing the leftmost characters of the specified length. Returns an empty string if the input is
        /// null, empty, or if length is less than or equal to zero. Returns the original string if length is greater
        /// than or equal to the string's length.</returns>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0)
            {
                return string.Empty;
            }
            if (length >= value.Length)
            {
                return value;
            }
            return value.Substring(0, length);
        }
        /// <summary>
        /// Returns a substring containing the specified number of characters from the end of the input string.
        /// </summary>
        /// <param name="value">The string from which to extract the substring. Can be null or empty.</param>
        /// <param name="length">The number of characters to retrieve from the end of the string. Must be greater than zero to return a
        /// non-empty result.</param>
        /// <returns>A substring of the specified length from the end of the input string. Returns the original string if the
        /// specified length is greater than or equal to the string's length. Returns an empty string if the input is
        /// null, empty, or if the specified length is less than or equal to zero.</returns>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0)
            {
                return string.Empty;
            }
            if (length >= value.Length)
            {
                return value;
            }
            return value.Substring(value.Length - length, length);
        }
    }
}
