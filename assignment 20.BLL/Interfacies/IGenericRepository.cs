using assignment_20.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Interfacies
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        //IEnumrable filter data in APPlication and get all data from DB and filter it in Application
        //IQueable filterd data first in DB and after that get data that filterd
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
