using System;
using System.Collections.Generic;
using EID;

namespace EID
{
    internal class Program
    {
        // Delegate declaration
        delegate int Numbers(int[] numbers);

        static void Main(string[] args)
        {
            #region --< EXTENSION METHOD EXAMPLES >------------------------------------------------------------
            Console.WriteLine("hello".IsCapitalized());
            Console.WriteLine("Bob Dust".Left(3));
            Console.WriteLine("Bob Dust".Right(4));

            var consumer = new Consumer { CustomerId = 1, Name = "Alice", TotalAmount = 100.0 };
            var vendor = new Vendor { CustomerId = 2, Name = "Acme Corp", TotalAmount = 5000.0 };
            var businessAssociates = new List<IBusinessAssociate> { consumer, vendor };

            foreach (var associate in businessAssociates)
            {
                Console.WriteLine($"ID: {associate.Id}, \tName: {associate.Name}, Total Amount: {associate.TotalAmount}");
            }
            #endregion
            //--< DELEGATE EXAMPLES >------------------------------------------------------------

            int[] sevens = new int[]
            {
                42, 7, 14, 63, 21, 70, 49, 28, 35, 56
            };

            Numbers math = Sum;
            int result = math(sevens);
            Console.WriteLine($"The sum is {result}");

            math = Max;
            result = math(sevens);
            Console.WriteLine($"The maximum number is {result}");
            Console.ReadKey();
        }

        // Method that matches the delegate
        /// <summary>
        /// Calculates the sum of all elements in the specified array of integers.
        /// </summary>
        /// <param name="nums">An array of integers to be summed. Cannot be null.</param>
        /// <returns>The total sum of the elements in the array. Returns 0 if the array is empty.</returns>
        static int Sum(int[] nums)
        {
            int total = 0;
            foreach (var number in nums)
                total += number;
            return total;
        }
        /// <summary>
        /// Returns the largest value in the specified array of integers.
        /// </summary>
        /// <param name="nums">An array of integers to search for the maximum value. Cannot be null or empty.</param>
        /// <returns>The maximum integer value found in the <paramref name="nums"/> array.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="nums"/> is null or contains no elements.</exception>
        static int Max(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                throw new ArgumentException("Input array cannot be null or empty.");
            int max = nums[0];
            foreach (var number in nums)
            {
                if (number > max)
                    max = number;
            }
            return max;
        }
    }
}