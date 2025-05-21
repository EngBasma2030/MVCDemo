using Cemo.BLL.DTO.EmployeeDTO;
using Cemo.BLL.Services.AttachmentService;
using Cemo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Demo.PL.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService, IDepartmentService _departmentService ,
        ILogger<EmployeeController> _logger, IWebHostEnvironment _environment , IAttachmentService attachmentService) : Controller
    {

        public IActionResult Index(string? EmployeeSearchName)
        {
            //TempData.Keep();
            // Binding through view's dictionary : transfer data from action to view 
            // 1. view date 
            //ViewData["Message"] = "Hello ViewData";
            //string ViewDataMessage = ViewData["Message"] as string;
            //// 2. view bag
            //ViewBag.Message = " Hallo ViewBag";
            //string ViewBagMessage = ViewBag.Message;

            dynamic employees = null!;
            if (string.IsNullOrEmpty(EmployeeSearchName))
            {
                 employees = _employeeService.GetAllEmployees();

            }
            else
            {
                 employees = _employeeService.SearchEmployeesByName(EmployeeSearchName);

            }
            return View(employees);
        }

        #region Create Employee
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService _departmentService*/)
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeCreatedDto = new CreatedEmployeeDto()
                    {
                        Name = employeeDto.Name,
                        Address = employeeDto.Address,
                        Age = employeeDto.Age,
                        Email = employeeDto.Email,
                        IsActive = employeeDto.IsActive,
                        PhoneNumber = employeeDto.PhoneNumber,
                        EmployeeType = employeeDto.EmployeeType,
                        Gender = employeeDto.Gender,
                        HiringDate = employeeDto.HiringDate,
                        Salary = employeeDto.Salary,
                        DepartmentId = employeeDto.DepartmentId,
                        Image = employeeDto.Image,
                    };

                    // create => created , saveChanges()
                    /*int result =*/ _employeeService.CreateEmployee(employeeCreatedDto);
                    // update => updated , saveChanges()
                    // edit daprtmentId => modified , saveChanges()

                    // delete => deleted , saveChanges()


                    // create , update , edit , delete => saveChanges()

                    // 3. TempData 
                    //if (result > 0)
                    //{
                    //    TempData["Message"] = "Employee Created Successfully";
                    //    return RedirectToAction(nameof(Index));
                    //}


                    //else
                    //{
                    //    TempData["Message"] = "Employee Creation failed";

                    //    ModelState.AddModelError(string.Empty, "Employee Can't Be Created !!");
                    //}


                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => log Error in Console and return same view with error message
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => log Error in file | Table in Database and return Error View 
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(employeeDto);
        }
        #endregion

        #region Details Of Employee
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id/* , [FromServices] IDepartmentService _departmentService*/)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new EmployeeViewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View(employeeDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var employeeUpdatedDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    Email = viewModel.Email,
                    IsActive = viewModel.IsActive,
                    PhoneNumber = viewModel.PhoneNumber,
                    EmployeeType = viewModel.EmployeeType,
                    Gender = viewModel.Gender,
                    HiringDate = viewModel.HiringDate,
                    Salary = viewModel.Salary,
                };
                int result = _employeeService.UopdateEmployee(employeeUpdatedDto);
                    if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Created !!");

                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => log Error in Console and return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    // 2. Deployment => log Error in file | Table in Database and return Error View 
                    _logger.LogError(ex.Message);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, " Employee can't be deleted !!");
                    return RedirectToAction(nameof(Delete));
                }
            }
            catch (Exception ex)
            {
                // log exception
                if (_environment.IsDevelopment())
                {
                    // 1. Development => log Error in Console and return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    // 2. Deployment => log Error in file | Table in Database and return Error View 
                    _logger.LogError(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
