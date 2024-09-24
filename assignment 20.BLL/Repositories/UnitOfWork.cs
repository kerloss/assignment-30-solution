using assignment_20.BLL.Interfacies;
using assignment_20.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            IemployeeRepository = new EmployeeRepository(_dbContext);
            IdepartmentRepository = new DepartmentRepository(_dbContext);
        }

        public IEmployeeRepository IemployeeRepository { get ; set ; } //Automatic properity
        public IDepartmentRepository IdepartmentRepository { get ; set ; }

        public int savechange()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose() //To close connection of Network by every request
        {
            _dbContext.Dispose();
        }
    }
}
