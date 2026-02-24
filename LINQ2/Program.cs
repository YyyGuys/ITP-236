using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2
{
    internal class Program
    {
        static List<Student> students = Student.Students;
        static List<Course> courses = Course.Courses;
        static List<Roster> rosters = Roster.Rosters;

        static List<Customer> customers = Customer.Customers;
        static List<SalesOrder> salesOrders = SalesOrder.SalesOrders;

        static void Main(string[] args)
        {
            SchoolExamples();
            SalesExamples();

            Console.ReadKey();
        }
        #region School Examples
        static void SchoolExamples()
        {
            var studentSchedule = StudentQueryExample(); DisplayStudent("Query", studentSchedule);
            studentSchedule = MethodExample(); DisplayStudent("Method", studentSchedule);
            studentSchedule = SelectManyExample(); DisplayStudent("Select Many",studentSchedule);
            studentSchedule = GroupByExample(); DisplayStudent("Group By",studentSchedule);
            var courseRoster = CourseQueryExample(); DisplayCourse("Course Query", courseRoster);
        }
        static dynamic StudentQueryExample()
        {
            //--< QUERY METHOD >--//
            /* The Join method has this signature:
               Join(outer,                  "students"
                    inner,                  "rosters"
                    outerKeySelector,       "stu.StudentId"
                    innerKeySelector,       "ros.StudentId"
                    resultSelector          "result"
               ) */
            //--< For each Student, gather all of the student's Courses >--//
            var studentSchedule =
                (from stu in students
                 join ros in rosters on stu.StudentId equals ros.StudentId
                 join course in courses on ros.CourseId equals course.CourseId
                 //--< Note that the StudentId alone makes the Key unique. We add Name as an added property >--//
                 group course by new { ros.StudentId, stu.Name }    //--< StudentId and Name are the Key <<<
                    into courseGroup        //--< Creates a Group <<<
                 select new                 //--< Anonymous Class <<<
                 {
                     StudentId = courseGroup.Key.StudentId,    //--< Note that we don't have to name the property StudentId here <<<
                     Student = courseGroup.Key.Name,
                     Count = courseGroup.Count(),              //--< Do "collection" things like Count on the Group <<<
                     Courses = courseGroup.ToList()            //--< Create a collection of Courses from the Group <<<
                 }
                );
            return studentSchedule;
        }
        static dynamic MethodExample()
        {
            //--< Same result using Method style >--//
            var studentSchedule = students
                    .Join(rosters,
                          stu => stu.StudentId,
                          ros => ros.StudentId,
                          (stu, ros) => new { stu, ros })
                    .Join(courses,
                          temp => temp.ros.CourseId,
                          course => course.CourseId,
                          (temp, course) => new
                          {
                              temp.stu.StudentId,
                              temp.stu.Name,
                              Course = course
                          })
                    .GroupBy(x => new { x.StudentId, x.Name })
                    .Select(g => new
                    {
                        StudentId = g.Key.StudentId,
                        Student = g.Key.Name,
                        Count = g.Count(),
                        Courses = g.Select(x => x.Course).ToList()
                    });
            return studentSchedule;
        }
        static dynamic SelectManyExample()
        {
            //--< Same reesult using SelectMany >--//
            var studentSchedule = students
                    .SelectMany(stu => rosters.Where(r => r.StudentId == stu.StudentId),
                                (stu, ros) => new { stu, ros })
                    .SelectMany(x => courses.Where(c => c.CourseId == x.ros.CourseId),
                                (x, course) => new
                                {
                                    x.stu.StudentId,
                                    x.stu.Name,
                                    Course = course
                                })
                    .GroupBy(x => new { x.StudentId, x.Name })
                    .Select(g => new
                    {
                        StudentId = g.Key.StudentId,
                        Student = g.Key.Name,
                        Count = g.Count(),
                        Courses = g.Select(x => x.Course).ToList()
                    });
            return studentSchedule;
        }
        static dynamic GroupByExample()
        {
            /*
                1. After the joins, each row looks like:
                {
                    StudentId,
                    StudentName,
                    Course
                }
                2. GroupBy groups those rows by:
                    new { x.StudentId, x.StudentName }
                    So each group represents one student.
                3. Inside each group, we now have:
                    - All the courses that student is taking
                    - The ability to call .Count(), .ToList(), .Select(), etc.
                4. The final projection builds a clean object:
                    - StudentId
                    - Student name
                    - Number of courses
                    - List of course objects


            */
            var studentSchedule = students
                    .Join(rosters,
                          stu => stu.StudentId,
                          ros => ros.StudentId,
                          (stu, ros) => new { stu, ros })
                    .Join(courses,
                          temp => temp.ros.CourseId,
                          course => course.CourseId,
                          (temp, course) => new
                          {
                              StudentId = temp.stu.StudentId,
                              StudentName = temp.stu.Name,
                              Course = course
                          })
                    .GroupBy(x => new { x.StudentId, x.StudentName })       //--< GROUP BY <<<
                    .Select(g => new
                    {
                        StudentId = g.Key.StudentId,
                        Student = g.Key.StudentName,
                        Count = g.Count(),
                        Courses = g.Select(x => x.Course).ToList()
                    });
            return studentSchedule;
        }
        #region School Displays
        static void DisplayStudent(string example, dynamic studentSchedule)
        {
            Console.WriteLine($"\n{example} Example");
            //--< List each student and the student's courses >--//
            foreach (var ss in studentSchedule)
            {
                Console.WriteLine($"{ss.StudentId}\t{ss.Student}");
                foreach (var cc in ss.Courses)
                {
                    Console.WriteLine($"\t\t{cc.Name}");
                }
            }
        }
        static dynamic CourseQueryExample()
        {
            //--< QUERY METHOD >--//
            var courseRoster = (
                from stu in students
                join ros in rosters on stu.StudentId equals ros.StudentId
                join course in courses on ros.CourseId equals course.CourseId
                group stu by new { course.CourseId, course.Name }
                    into stuGroup
                select new //CourseRoster
                {
                    CourseId = stuGroup.Key.CourseId,
                    Course = stuGroup.Key.Name,
                    Students = stuGroup.ToList(),
                    RosterCount = stuGroup.Count()
                }
           );
            //var classRoster = courses.GroupBy(
            //    course => course.CourseId,
            //    course => rosters.Where(r => r.CourseId == course.CourseId)
            //        .Select(r => students.FirstOrDefault(s => s.StudentId == r.StudentId)),
            //        (key, groupedStudents) => new
            //        {
            //            CourseId = key,
            //            Course = courses.First(c => c.CourseId == key).Name,
            //            Students = groupedStudents.SelectMany(st => st).ToList()
            //        }
            //);
            return courseRoster;
        }
        static void DisplayCourse(string example, dynamic courseRoster)
        {
            Console.WriteLine($"\n{example} Example");
            foreach (var cc in courseRoster)
            {
                Console.WriteLine($"{cc.CourseId}\t{cc.RosterCount}\t{cc.Course}");
                foreach (var stu in cc.Students)
                {
                    Console.WriteLine($"\t\t{stu.Name}");
                }
            }
        }
        #endregion
        #endregion
        #region Sales Examples
        static void SalesExamples()
        {
            var customerOrders = SalesQueryExample();       DisplaySales("Sales Query", customerOrders);
            customerOrders = AnotherSalesQueryExample();    DisplayOrders("Another Sales Query", customerOrders);
            var regions = GroupByRegionExample();           DisplayRegions("Regions Group By Query", regions);
            var selectedOrders = SalesContainsExample();    DisplayContains("Sales Contains", selectedOrders);
        }
        static dynamic SalesQueryExample()
        {
            var customerOrders = from customer in customers
                                 join salesOrder in salesOrders
                                  on customer.CustomerId equals salesOrder.CustomerId
                                 select new
                                 {
                                     Customer = customer.Name,
                                     customer.CustomerId,
                                     salesOrder.OrderDate,
                                     salesOrder.OrderTotal
                                 };
            return customerOrders;
        }
        static dynamic AnotherSalesQueryExample()
        {
            var orders = from customer in customers
                         join salesOrder in salesOrders
                            on customer.CustomerId equals salesOrder.CustomerId
                            into ordersGroup    //--< Creates a Group <<<
                         select new             //--< Anonymous class <<<
                         {
                             Customer = customer.Name,
                             customer.CustomerId,
                             Count = ordersGroup.Count(),
                             OrderTotal = ordersGroup.Sum(grp => grp.OrderTotal),   //--< Does "collection" things with the Group <<<
                             SalesOrders = ordersGroup.ToList()
                         };
            return orders;
        }
        static dynamic GroupByRegionExample()
        {
            var regional = from c in customers
                           join so in salesOrders
                           on c.CustomerId equals so.CustomerId
                           into custOrders
                           group new { c, Orders = custOrders }
                           by c.Region into regionGroup
                           select new
                           {
                               region = regionGroup.Key,
                               Customers = regionGroup
                           };
            return regional;
        }
        static dynamic SalesContainsExample()
        {
            //--< Contains >--//
            int[] customerIds = { 1, 3 };
            var selectedOrders = from order in salesOrders
                                 where customerIds
                                 .Contains(order.CustomerId)
                                 select order;
            return selectedOrders;
        }
        #region Sales Displays
        static void DisplaySales(string example, dynamic customerOrders)
        {
            Console.WriteLine($"\n{example} Example");
            foreach (var co in customerOrders)
            {
                Console.WriteLine($"{co.Customer}" +
                    $"\t{co.CustomerId}" +
                    $"\t{co.OrderDate}" +
                    $"\t{co.OrderTotal.ToString("C")}");
            }
        }
        static void DisplayOrders(string example, dynamic orders)
        {
            Console.WriteLine($"\n{example} Example");
            foreach (var order in orders)       //--< Uses the anonymous class <<<
            {
                Console.WriteLine($"{order.CustomerId}" +
                    $"\t{order.Customer}" +
                    $"\t{order.Count}" +
                    $"\t{order.OrderTotal.ToString("C")}");
            }
        }
        static void DisplayRegions(string example, dynamic regional)
        {
            Console.WriteLine($"\n{example} Example");
            foreach (var region in regional)
            {
                Console.WriteLine($"Region: {region.region}");
                foreach (var cust in region.Customers)
                {
                    Console.WriteLine($"\t{cust.c.Name}");
                    foreach (var order in cust.Orders)
                    {
                        Console.WriteLine($"\t\tOrderId: {order.OrderId}" +
                            $"\tOrder Date: {order.OrderDate}" +
                            $"\tAmount: {order.OrderTotal.ToString("C")}");
                    }
                }
            }
        }
        static void DisplayContains(string example, dynamic selectedOrders)
        {
            Console.WriteLine($"\n{example} Example");
            foreach (var selectedOrder in selectedOrders)
            {
                Console.WriteLine(selectedOrder.ToString());
            }
        }
        #endregion
        #endregion
    }
}
