using assignment_20.BLL.Interfacies;
using assignment_20.DAL.Data;
using assignment_20.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        //private readonly AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext DbContext) : base(DbContext) //Ask CLR for creating object from DbContext
        {
            //_appDbContext = DbContext;
        }

        public IQueryable<Department> GetDepartmentByName(string name)
        {
            return _appDbContext.Departments.Where(D => D.Name.ToLower() == name.ToLower());
            //return _appDbContext.Departments.Where(D => D.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
