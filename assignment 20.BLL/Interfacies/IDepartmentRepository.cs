﻿using assignment_20.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Interfacies
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //IEnumerable<Department> GetAll();
        //Department GetById(int id);
        //int Add (Department department);
        //int Update (Department department);
        //int Delete (Department department);

        IQueryable<Department> GetDepartmentByName(string name);
    }
}
