using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.Entity;
using Employee.App.Data.Entities;

namespace Employee.App.Data
{
    public class Context : DbContext
    {
        public DbSet<Employe> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}