#define Test1
//#define Test2
//#define Test3
//#define Test4
using Microsoft.EntityFrameworkCore;

namespace SchoolModel.Tests;

public class StudentGpaTests
{
    private SchoolContext GetContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new SchoolContext(options);
    }
#if Test1 
    [Fact]
    public void GPA_ReturnsCorrectAverage()
    {
        /*
         *  Arrange
         *  Act
         *  Assert
        */
        //<----- Arrange: Set up data and objects ----->//
        using var context = GetContext("GpaTestDB");

        var student = new Student
        {
            StudentId = 1,
            FirstName = "Ada",
            LastName = "Lovelace"
        };

        context.Students.Add(student);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 1, Student = student, CourseId = 101, Grade = "A" }, // 4.0
            new Enrollment { EnrollmentId = 2, Student = student, CourseId = 102, Grade = "B" }  // 3.0
        );

        context.SaveChanges();

        //<----- Act: Call the method being tested ----->//
        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 1)
            .GPA;

        //<----- Assert: Verify the result is what you expect ----->//
        Assert.Equal(3.5, result);
    }
#endif 
#if Test2
    [Fact]
    public void GPA_ReturnsNull_WhenNoGrades()
    {
        using var context = GetContext("NoGradesDB");

        var student = new Student
        {
            StudentId = 2,
            FirstName = "Grace",
            LastName = "Hopper"
        };

        context.Students.Add(student);
        context.SaveChanges();

        var result = context.Students.First().GPA;

        Assert.Null(result);
    }
#endif
#if Test3 
    //--< This verifies that the GPA is simply the grade points when only one enrollment exists. >--//
    [Fact]
    public void GPA_ReturnsSingleGradeValue()
    {
        using var context = GetContext("SingleGradeDB");

        var student = new Student
        {
            StudentId = 3,
            FirstName = "Alan",
            LastName = "Turing"
        };

        context.Students.Add(student);

        context.Enrollments.Add(
            new Enrollment { EnrollmentId = 10, Student = student, CourseId = 201, Grade = "A" } // 4.0
        );

        context.SaveChanges();

        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 3)
            .GPA;

        Assert.Equal(4.0, result);
    }
    //--< Confirm that the average is calculated correctly >--//
    [Fact]
    public void GPA_ReturnsAverageOfMultipleGrades()
    {
        using var context = GetContext("MultipleGradesDB");

        var student = new Student
        {
            StudentId = 4,
            FirstName = "Katherine",
            LastName = "Johnson"
        };

        context.Students.Add(student);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 20, Student = student, CourseId = 101, Grade = "A" }, // 4.0
            new Enrollment { EnrollmentId = 21, Student = student, CourseId = 102, Grade = "C" }  // 2.0
        );

        context.SaveChanges();

        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 4)
            .GPA;

        Assert.Equal(3.0, result);
    }
    //--< GPA Ignores Enrollments with No Grade >--//
    [Fact]
    public void GPA_IgnoresEnrollmentsWithNoGrade()
    {
        using var context = GetContext("IgnoreNullGradesDB");

        var student = new Student
        {
            StudentId = 5,
            FirstName = "Margaret",
            LastName = "Hamilton"
        };

        context.Students.Add(student);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 30, Student = student, CourseId = 101, Grade = "A" }, // 4.0
            new Enrollment { EnrollmentId = 31, Student = student, CourseId = 102, Grade = null } // ignored
        );

        context.SaveChanges();

        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 5)
            .GPA;

        Assert.Equal(4.0, result);
    }
    //--< GPA Returns Null When All Grades Are Null >--//
    [Fact]
    public void GPA_ReturnsNull_WhenAllGradesAreNull()
    {
        using var context = GetContext("AllNullGradesDB");

        var student = new Student
        {
            StudentId = 6,
            FirstName = "Tim",
            LastName = "Berners-Lee"
        };

        context.Students.Add(student);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 40, Student = student, CourseId = 101, Grade = null },
            new Enrollment { EnrollmentId = 41, Student = student, CourseId = 102, Grade = null }
        );

        context.SaveChanges();

        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 6)
            .GPA;

        Assert.Null(result);
    }
    //--< GPA Handles All Grade Types (A–F) >--//
    [Fact]
    public void GPA_HandlesAllLetterGrades()
    {
        using var context = GetContext("AllGradesDB");

        var student = new Student
        {
            StudentId = 7,
            FirstName = "Barbara",
            LastName = "Liskov"
        };

        context.Students.Add(student);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 50, Student = student, CourseId = 101, Grade = "A" }, // 4.0
            new Enrollment { EnrollmentId = 51, Student = student, CourseId = 102, Grade = "B" }, // 3.0
            new Enrollment { EnrollmentId = 52, Student = student, CourseId = 103, Grade = "C" }, // 2.0
            new Enrollment { EnrollmentId = 53, Student = student, CourseId = 104, Grade = "D" }, // 1.0
            new Enrollment { EnrollmentId = 54, Student = student, CourseId = 105, Grade = "F" }  // 0.0
        );

        context.SaveChanges();

        var result = context.Students
            .Include(s => s.Enrollments)
            .First(s => s.StudentId == 7)
            .GPA;

        Assert.Equal(2.0, result); // (4+3+2+1+0) / 5
    }
#endif
#if Test4
    //--< Test for Mixed Null Grades + Credits >--//
    [Fact]
    public void GPA_WeightedByCourseCredits()
    {
        using var context = GetContext("WeightedGpaDB");

        // Arrange
        var student = new Student
        {
            StudentId = 10,
            FirstName = "Ada",
            LastName = "Lovelace"
        };

        var courseA = new Course
        {
            CourseId = 1001,
            Tag = "MTH-101",
            Title = "Calculus",
            Credits = 3
        };

        var courseB = new Course
        {
            CourseId = 1002,
            Tag = "ENG-200",
            Title = "English Literature",
            Credits = 4
        };

        context.Students.Add(student);
        context.Courses.AddRange(courseA, courseB);

        context.Enrollments.AddRange(
            new Enrollment { EnrollmentId = 5001, Student = student, Course = courseA, Grade = "A" }, // 4.0 × 3
            new Enrollment { EnrollmentId = 5002, Student = student, Course = courseB, Grade = "C" }  // 2.0 × 4
        );

        context.SaveChanges();

        // Act
        var result = context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .First(s => s.StudentId == 10)
            .GPA;

        // Assert
        Assert.Equal(20.0 / 7.0, result!.Value, 3);     //--< Compare to 3 decimal places <<<
    }
#endif
}