// See https://aka.ms/new-console-template for more information
using Collection;
Console.WriteLine("LINQ");
int[] sevens = new int[]
            {
                42, 7, 14, 63, 21, 70, 49, 28, 35, 56
            };
//--< Use LINQ >--  Language INtegrated Query
int sum = sevens.Sum();
int max = sevens.Max();
double avg = sevens.Average();

//--< Use Student Class >--//
var students = Student.Students;
float gpa = students.Average(s => s.GPA);       //--< => Lambda Expression: s => s.GPA
Console.WriteLine($"Average GPA: {gpa}");
double idAvg = students.Average(s => s.StudentId);
Console.WriteLine($"Average Student ID: {idAvg}");

var deansList = students.Where(s => s.GPA >= 3.0);
Console.ReadKey();
