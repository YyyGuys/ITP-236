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

            //--< DELEGATE EXAMPLES >------------------------------------------------------------

            int[] sevens = new int[]
            {
                42, 7, 14, 63, 21, 70, 49, 28, 35, 56
            };

            Numbers math = SumNumbers;
            int result = math(sevens);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        // Method that matches the delegate
        static int SumNumbers(int[] nums)
        {
            int total = 0;
            foreach (var number in nums)
                total += number;
            return total;
        }
    }
}