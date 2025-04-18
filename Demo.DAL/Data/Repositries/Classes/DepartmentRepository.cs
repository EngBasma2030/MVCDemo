﻿using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    // Primary Constructor .Net 8 C#12
    public class DepartmentRepository(AppDbContext dbContext) :GenericRepository<Department>(dbContext) ,IDepartmentRepository
    {
       
    }
}
