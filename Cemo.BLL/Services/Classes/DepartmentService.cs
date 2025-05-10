using Cemo.BLL.DTO.DepartmentDTO;
using Cemo.BLL.Factories;
using Cemo.BLL.Services.Interfaces;
using Demo.DAL.Data.Repositries.Classes;
using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services.Classes
{
    // primary constructor
    public class DepartmentService(/*IDepartmentRepository _departmentRepository*/ IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        // private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        // Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        // Update Department 
        public int UpdateDepartment(IUpdateDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Delate(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        //public int AddDepartment(CreatedDepartmentDto departmentDto)
        //{
        //    var department = departmentDto.ToEntity();
        //    _unitOfWork.DepartmentRepository.Add(department);
        //    return _unitOfWork.SaveChanges();
        //}

        IEnumerable<DTO.DepartmentDTO.DepartmentDto> IDepartmentService.GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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

        DTO.DepartmentDTO.DepartmentDetailsDto IDepartmentService.GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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

        //public int UpdateDepartment(IUpdateDepartmentDto departmentDto)
        //{
        //    _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
        //    return _unitOfWork.SaveChanges();
        //}
    }
}
