using Cemo.BLL.DTO;
using Cemo.BLL.Services;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService ,
        ILogger<DepartmentController> _logger , IWebHostEnvironment _environment) : Controller
    {
        // private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    int result = _departmentService.AddDepartment(departmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't  Be Created !!");
                    }
                }
                catch (Exception ex)
                {
                    // log exception
                    if(_environment.IsDevelopment())
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
            return View(departmentDto);

        }
        #endregion

        #region Details Of Department
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); // 400
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); //404
            return View(department);
        }
        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); //404
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id ,DepartmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation.Value
                };
                int result = _departmentService.UpdateDepartment(updatedDepartment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can't Be Edit !!!");
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
            return View(viewModel);
        }
        #endregion
    }
}
