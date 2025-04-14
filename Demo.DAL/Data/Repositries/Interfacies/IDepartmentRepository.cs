using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Interfacies
{
    public interface IDepartmentRepository
    {
        // Get All 
        IEnumerable<Department> GetAll(bool withTracking = false); 

        // Get By Id 
        Department GetById(int id);

        // Insert 
        int Add(Department Entity);

        // Update 
        int Update(Department Entity);

        // Delete
        int Delate(Department Entity);
    }
}
