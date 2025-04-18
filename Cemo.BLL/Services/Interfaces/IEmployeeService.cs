using Cemo.BLL.DTO.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        // Get All Employees
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employee);
        int UopdateEmployee(UpdatedEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
