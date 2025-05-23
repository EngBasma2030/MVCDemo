﻿using AutoMapper;
using Cemo.BLL.DTO.EmployeeDTO;
using Cemo.BLL.Services.AttachmentService;
using Cemo.BLL.Services.Interfaces;
using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services.Classes
{
    public class EmployeeService( IUnitOfWork unitOfWork, IMapper _mapper , IAttachmentService attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var Employees = unitOfWork.EmployeeRepository.GetAll(withTracking);
            // src = IEnumerable<Employee>
            // Dest = IEnumrable<EmployeeDto>
            var returnedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            //var returnedEmployees = Employees.Select(emp => new EmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString()
            //});
            return returnedEmployees;
        }


        public IEnumerable<EmployeeDto> SearchEmployeesByName(string  name)
        {
            var Employees = unitOfWork.EmployeeRepository.GetEmployeeByName(name.ToLower());
            // src = IEnumerable<Employee>
            // Dest = IEnumrable<EmployeeDto>
            var returnedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            //var returnedEmployees = Employees.Select(emp => new EmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString()
            //});
            return returnedEmployees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var Employee = unitOfWork.EmployeeRepository.GetById(id);
            //if (Employee == null) return null;
            //else
            //{
            //    var returnedEmp = new EmployeeDetailsDto()
            //    {
            //        Id = Employee.Id,
            //        Name = Employee.Name,
            //        Age = Employee.Age,
            //        Email = Employee.Email,
            //        Salary = Employee.Salary,
            //        IsActive = Employee.IsActive,
            //        EmployeeType = Employee.EmployeeType.ToString(),
            //        Gender = Employee.Gender.ToString(),
            //        PhoneNumber = Employee.PhoneNumber,
            //        HiringDate = DateOnly.FromDateTime(Employee.HiringDate),
            //        CreatedBy = 1,
            //        LastModifiedBy = 1,
            //    };
            //    return returnedEmp;
            //}

            return Employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(Employee);
           


        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if(employeeDto.Image is not null)
            {
                Employee.ImageName = attachmentService.Uplooad(employeeDto.Image, "images");
            }
            unitOfWork.EmployeeRepository.Add(Employee);
            return unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id) //ممسحش من علي السيرفر  Soft Delete  لو شغاله  
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);

            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                employee.ImageName = null;
               unitOfWork.EmployeeRepository.Update(employee) ;
                int result = unitOfWork.SaveChanges();
                if (result > 0)
                {
                    attachmentService.Delete(employee.ImageName, "images");
                    return true;
                }
                else
                    return false;
            }
        }

        public int UopdateEmployee(UpdatedEmployeeDto employee)
        {
            unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employee));
            return unitOfWork.SaveChanges();
        }
    }
}
