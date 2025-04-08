using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    // Primary Constructor .Net 8 C#12
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext = dbContext; // null

        public int Add(Department Entity) // object member method
        {
            _dbContext.Departments.Add(Entity); // added
            return _dbContext.SaveChanges(); // update database
        }

        public int Delate(Department Entity)
        {
            _dbContext.Departments.Remove(Entity); // deleted local 
            return _dbContext.SaveChanges(); // update database
        }

        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Departments.ToList(); 
            }
            else
                return _dbContext.Departments.AsNoTracking().ToList(); 
            
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id); // find by primary Key
            // find<Department>(id)
        }

        public int Update(Department Entity)
        {
            _dbContext.Departments.Update(Entity); // update local [modified]
            return _dbContext.SaveChanges(); // update database
        }
    }
}
