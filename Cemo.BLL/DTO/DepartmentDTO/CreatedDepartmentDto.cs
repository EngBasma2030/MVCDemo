using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.DTO.DepartmentDTO
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name Is Require !!!")]
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
        public string? Description { get; set; }
    }
}
