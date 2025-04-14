using Cemo.BLL.DTO;
using Cemo.BLL.Factories;
using Demo.DAL.Data.Repositries.Classes;
using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services
{
    // primary constructor
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        // private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        // Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            //1. manual mapping 
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    //Id = D.Id,
            //    //Name = D.Name,
            //    //Description = D.Description,
            //    //Code = D.Code,
            //    //DateOfCreation = DateOnly.FromDateTime(D.CreatedOn.Value)
            //});
            //return departmentsToReturn;

            // 2. Extension Method
            return departments.Select(D => D.ToDepatmentDto());
        }

        // Get Department By Id
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            //if (department == null) return null;
            //else
            //{
            // var departmentToReturn = new DepartmentDetailsDto()
            // {
            // Id = department.Id,
            // Name = department.Name,
            // Description = department.Description,
            // Code = department.Code,
            // CreatedBy = department.CreatedBy,
            // LastModifiedBy = department.LastModifiedBy,
            // CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value)
            // };
            // return departmentToReturn;
            // }

            // manual mapping
            // auto Mapper
            // Constructor Mapping
            // Extension Method

            // 1. manual mapping
            //return department is null ? null : new DepartmentDetailsDto(department)
            //{
            //    //Id = department.Id,
            //    //Name = department.Name,
            //    //Description = department.Description,
            //    //Code = department.Code,
            //    //CreatedBy = department.CreatedBy,
            //    //LastModifiedBy = department.LastModifiedBy,
            //    //CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value)
            //};

            // 2. Extention Method 
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        // Add Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepository.Add(department);
        }

        // Update Department 
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                int result = _departmentRepository.Delate(department);
                return result > 0 ? true : false;
            }
        }
    }
}
