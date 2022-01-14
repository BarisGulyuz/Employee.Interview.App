using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.App.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employe Employee { get; set; }
        public string Url { get; set; }
       
    }
}