// See https://aka.ms/new-console-template for more information
using EID;
using System.Runtime.CompilerServices;
using System.Linq;

// IDE0300: Collection initialization can be simplified
int[] numbers = [
    42, 7, 14, 63, 21, 70, 49, 28, 35, 56
];
delegate int Numbers(int[] numbers);

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
// Move delegate declaration above top-level statements to fix CS8803


Numbers sum = EidDelegate.SumArray;



