﻿using Demo.DAL.Data.Configurations;
using Demo.DAL.Models;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class AppDbContext : DbContext
    {       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=MVC_C43;Trusted_Connection=true;MultipleActiveResultSets=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; } // table
        public DbSet<Employee> Employees { get; set; }
    }
}
