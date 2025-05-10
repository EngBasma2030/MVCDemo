using Cemo.BLL.DTO.DepartmentDTO;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Factories
{
    static public class DepartmentFactory
    {
        public static DepartmentDto ToDepatmentDto (this Department D)
        {
            return new DepartmentDto()
            {
                Id = D.Id,
                Name = D.Name,
                Description = D.Description,
                Code = D.Code,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn.Value)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                Code = department.Code,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy 
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this IUpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
