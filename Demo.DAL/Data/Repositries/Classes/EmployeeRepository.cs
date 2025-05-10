using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
       
        public IQueryable<Employee> GetEmployeeByName(string Name)
        {
            return dbContext.Employees.Where(e => e.Name.ToLower().Contains(Name));
        }
    }
}
