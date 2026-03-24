/*
    Install the following libraries. Use Version 8.0.0 (not latest):
• 	  Microsoft.Extensions.Configuration
• 	  Microsoft.Extensions.Configuration.Json
•     Microsoft.Extensions.Configuration.FileExtensions
 
    In appsettings.json, modify Properties:
•     Set - Copy to Output Directory → Copy if newer
*/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using SchoolModel;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("SchoolDb");

var options = new DbContextOptionsBuilder<SchoolContext>()
    .UseSqlServer(connectionString)
    .Options;

using var db = new SchoolContext(options);
var factory = new SchoolContextFactory();
using var context = factory.CreateDbContext(args);

// Only seed if empty
if (!context.Students.Any())
{
    var ada = new Student { FirstName = "Ada", LastName = "Lovelace", Major = "Math" };
    var grace = new Student { FirstName = "Grace", LastName = "Hopper", Major = "CS" };

    var cs101 = new Course { Tag = "CS101", Title = "Intro to Programming", Credits = 3 };
    var math200 = new Course { Tag = "MATH200", Title = "Discrete Math", Credits = 4 };

    context.Students.AddRange(ada, grace);
    context.Courses.AddRange(cs101, math200);

    context.SaveChanges();

    context.Enrollments.AddRange(
        new Enrollment { StudentId = ada.StudentId, CourseId = cs101.CourseId, Grade = "A" },
        new Enrollment { StudentId = grace.StudentId, CourseId = math200.CourseId, Grade = "A-" }
    );

    context.SaveChanges();
}

Console.WriteLine("Database seeded.");


// test query
Console.WriteLine($"Students: {db.Students.Count()}");

Console.WriteLine("EF Core Sandbox");
Console.WriteLine("----------------");

while (true)
{
    Console.Write("Command> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        break;

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) )
        break;

    try
    {
        switch (input.ToLower())
        {
            case "students":
                foreach (var s in context.Students)
                    Console.WriteLine($"{s.StudentId}: {s.FirstName} {s.LastName} ({s.Major})");
                break;

            case "courses":
                foreach (var c in context.Courses)
                    Console.WriteLine($"{c.CourseId}: {c.Tag} - {c.Title}");
                break;

            case "enrollments":
                foreach (var e in context.Enrollments)
                    Console.WriteLine($"{e.EnrollmentId}: {e.StudentId} -> {e.CourseId}");
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

//Console.ReadKey();