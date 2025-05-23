﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.EmployeeModel
{
    public class Employee : BaseEntity 
    {
        public String Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set;  }
        public decimal Salary { get; set; }
        public  Boolean IsActive { get; set; }
        public string? Email { get; set; }
        public string?  PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; } // FK Colomn
        // navigation property => [One]
        public virtual Department? Department { get; set; }

        public string? ImageName { get; set; }
    }
}
