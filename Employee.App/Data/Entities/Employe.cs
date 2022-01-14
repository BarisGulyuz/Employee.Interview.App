using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee.App.Data.Entities
{
    public class Employe
    {
        public Employe()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public int DeparmentId { get; set; }
        public Department Department { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public int ManagerId { get; set; }
        public List<Photo> Photos { get; set; }
    }


}