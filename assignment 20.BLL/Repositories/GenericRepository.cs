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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        // make it protected because when inherit it in other classes u will inherit it with private only
        private protected readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext DbContext)
        {
            _appDbContext = DbContext;
        }
        public int Add(T item)
        {
            //call with DataBase
            _appDbContext.Set<T>().Add(item);
            //OR
            //_appDbContext.Add(item);
            return _appDbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _appDbContext.Set<T>().Remove(item);
            //OR
            //_appDbContext.Remove(item);
            return _appDbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
            return (IEnumerable<T>)_appDbContext.Employees.Include(E=>E.departments).AsNoTracking().ToList();
            }
            else
            {
            return _appDbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
            //return _appDbContext.Find<T>(id);
        }

        public int Update(T item)
        {
            _appDbContext.Set<T>().Update(item);
            return _appDbContext.SaveChanges();
        }
    }
}
