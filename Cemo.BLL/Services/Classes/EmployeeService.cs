﻿using AutoMapper;
using Cemo.BLL.DTO.EmployeeDTO;
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
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var Employees = _employeeRepository.GetAll(withTracking);
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
            var Employee = _employeeRepository.GetById(id);
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
        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employee);
            return _employeeRepository.Add(Employee);
        }

        public bool DeleteEmployee(int id) // Soft Delete 
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }

        public int UopdateEmployee(UpdatedEmployeeDto employee)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employee));
        }
    }
}
