using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ1
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }

        public Customer() : this(-1, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public Customer(int customerId, string firstName, string lastName, string city, string state)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            State = state;
            SalesOrders = new List<SalesOrder>();
        }
        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(
                $"Customer: {FirstName} {LastName}\n" +
                $"\t\tCustomer Orders\n");
            foreach (var order in SalesOrders)
            {
                toString.Append(order.ToString());
            }
            return toString.ToString();
        }
        public static List<Customer> Customers => new List<Customer>
        {

               new Customer() {
                                CustomerId = 1,
                                FirstName = "James",
                                LastName = "River",
                                City = "Richmond",
                                State = "Virginia"
               },
               new Customer() {
                                CustomerId = 2,
                                FirstName = "Hugh",
                                LastName = "Gaknot",
                                City = "Short Pump",
                                State = "Virginia"
               },
               new Customer() {
                                CustomerId = 3,
                                FirstName = "Maggie",
                                LastName = "Walker",
                                City = "Richmond",
                                State = "Virginia"
               }
        };
    }
    public class SalesOrder
    {
        public int SalesOrderNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal OrderAmount { get; set; }


        public SalesOrder() : this(-1, -1, DateTime.Now, 0)
        {

        }

        public SalesOrder(int salesOrderNumber, int customerId, DateTime orderDate, Decimal amount)
        {
            SalesOrderNumber = salesOrderNumber;
            CustomerId = customerId;
            OrderDate = orderDate;
            OrderAmount = amount;
            Customer = null;
        }
        public override string ToString()
        {
            return $"SalesOrder: {SalesOrderNumber}\n" +
                $"\t{Customer?.FirstName} {Customer?.LastName}\n" +
                $"\tAmount: {OrderAmount}";
        }
        public static List<SalesOrder> SalesOrders => new List<SalesOrder>
            {
               new SalesOrder() {
                                SalesOrderNumber = 101,
                                CustomerId = 1,
                                OrderDate = new DateTime(2026,01,02),
                                OrderAmount = 125
               },
               new SalesOrder() {
                                SalesOrderNumber = 102,
                                CustomerId = 1,
                                OrderDate = new DateTime(2026,01,05),
                                OrderAmount = 2125
               },
               new SalesOrder() {
                                SalesOrderNumber = 105,
                                CustomerId = 1,
                                OrderDate = new DateTime(2026,02,12),
                                OrderAmount = 1133
               },
               new SalesOrder() {
                                SalesOrderNumber = 103,
                                CustomerId = 2,
                                OrderDate = new DateTime(2025,12,14),
                                OrderAmount = 377
                            },
               new SalesOrder() {
                                SalesOrderNumber = 104,
                                CustomerId = 2,
                                OrderDate = new DateTime(2025,01,07),
                                OrderAmount = 1833
               },
               new SalesOrder() {
                                SalesOrderNumber = 107,
                                CustomerId = 2,
                                OrderDate = new DateTime(2026,02,11),
                                OrderAmount = 2024
               },
               new SalesOrder() {
                                SalesOrderNumber = 109,
                                CustomerId = 2,
                                OrderDate = new DateTime(2026,02,11),
                                OrderAmount = 3480
               },
               new SalesOrder() {
                                SalesOrderNumber = 108,
                                CustomerId = 3,
                                OrderDate = new DateTime(2025,12,11),
                                OrderAmount = 1830
               },
               new SalesOrder() {
                                SalesOrderNumber = 111,
                                CustomerId = 3,
                                OrderDate = new DateTime(2026,02,15),
                                OrderAmount = 4130
               }
            };
    }
    public class SalesData
    {
        public List<Customer> Customers { get; private set; }
        public List<SalesOrder> SalesOrders { get; private set; }
        public SalesData()
        {
            Customers = Customer.Customers;
            SalesOrders = SalesOrder.SalesOrders;
            foreach (var customer in Customers)
            {
                customer.SalesOrders = SalesOrders.Where(so => so.CustomerId == customer.CustomerId).ToList();
            }
            foreach (var salesOrder in SalesOrders)
            {
                salesOrder.Customer = Customers.FirstOrDefault(c => c.CustomerId == salesOrder.CustomerId);
            }
        }
    }
}
