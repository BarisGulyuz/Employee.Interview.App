using Employee.App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.App.Models
{
    public class HomeViewModel
    {
        public Employe Employes { get; set; }
       public List<Photo> Photos { get; set; }
    }
}