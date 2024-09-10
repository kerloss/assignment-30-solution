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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext DbContext) //Ask CLR for creating object from DbContext
        {
            _appDbContext = DbContext;
        }
        public int Add(Department department)
        {
            //call with DataBase
            _appDbContext.Departments.Add(department);  //state Added
            return _appDbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _appDbContext.Departments.Remove(department);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _appDbContext.Departments.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _appDbContext.Departments.Find(id);
        }

        public int Update(Department department)
        {
            _appDbContext.Departments.Update(department);
            return _appDbContext.SaveChanges();
        }
    }
}
