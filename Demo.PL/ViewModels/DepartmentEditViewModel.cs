﻿namespace Demo.PL.ViewModels
{
    public class DepartmentEditViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateOnly? DateOfCreation { get; set; }
        public string? Description { get; set; }

    }
}
