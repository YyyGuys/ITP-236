// See https://aka.ms/new-console-template for more information
using Collection;
using LINQ1;
using System.Numerics;
Console.WriteLine("LINQ");
var data = new SalesData();
var customers = data.Customers;
var salesOrders = data.SalesOrders;
int[] sevens = new int[]
            {
                42, 7, 14, 63, 21, 70, 49, 28, 35, 56
            };
//--< Use LINQ >--  Language INtegrated Query
int sum = sevens.Sum();
int max = sevens.Max();
double avg = sevens.Average();

//--< LAZY LOADING >--//
int x = 0;
var array = sevens.Where(n => n >= x);  //--< => Lambda Expression: n => n > x
//--< HINT: array is a DELEGATE >--

x = 70;
var count = array.Count();   //--< The Where clause is not executed until we call Count() <<<
Console.WriteLine($"Count: {count})");

//--< Use Student Class >--//
var students = Student.Students;
float gpa = students.Average(s => s.GPA);       //--< => Lambda Expression: s => s.GPA
Console.WriteLine($"Average GPA: {gpa}");
double idAvg = students.Average(s => s.StudentId);
Console.WriteLine($"Average Student ID: {idAvg}");

//--< Another example of Lazy Loading >--//
var deansList = students.Where(s => s.GPA >= 3.0);   //--< deansList is a DELEGATE that represents the query to filter students with GPA >= 3.0, but it has not been executed yet.
count  = deansList.Count();
var results = students
    .Where(s => s.GPA >= 3.0)
    .Select(s => s.Name)
    .OrderBy(name => name)
    .ToArray();     //<--< The query is executed when we call ToArray() (no Lazy Loading), which retrieves the names of students with GPA >= 3.0, orders them alphabetically, and stores them in an array. <<<

Console.ReadKey();
