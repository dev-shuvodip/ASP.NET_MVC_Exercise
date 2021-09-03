using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_Exercise.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public string CustomID { get; set; }
    }

    public class CustomerData
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public long CustomerContact { get; set; }

    }
}