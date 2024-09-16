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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        //private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext DbContext) : base(DbContext)   //Ask CLR for creating object from DbContext
        {
            //_appDbContext = DbContext;
        }

        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _appDbContext.Employees.Where(e => e.Address.ToLower().Contains(address.ToLower()));
            //return _appDbContext.Employees.Where(e => e.Address.ToLower() == address.ToLower());
        }
    }
}
