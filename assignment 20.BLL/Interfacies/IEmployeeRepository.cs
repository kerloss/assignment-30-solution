using assignment_20.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Interfacies
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee GetById(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

        //IQueable filterd data first in DB and after that get data that filterd
        IQueryable<Employee> GetEmployeeByAddress(string address);

        IQueryable<Employee> GetEmployeeByName(string name);
    }
}
