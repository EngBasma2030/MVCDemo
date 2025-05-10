using Cemo.BLL.DTO.DepartmentDTO;

namespace Cemo.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto GetDepartmentById(int id);
        int UpdateDepartment(IUpdateDepartmentDto departmentDto);
    }
}