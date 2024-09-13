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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext DbContext)   //Ask CLR for creating object from DbContext
        {
            _appDbContext = DbContext;
        }
        public int Add(Employee employee)
        {
            //Call with DataBase
            _appDbContext.Employees.Add(employee);
            return _appDbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _appDbContext.Employees.Remove(employee);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _appDbContext.Employees.AsNoTracking().ToList();
        }

        public Employee GetById(int id)
        {
            //Find() => search localy first and after that search in DB
            return _appDbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            return _appDbContext.SaveChanges();
        }
    }
}
