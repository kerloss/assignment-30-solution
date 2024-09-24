using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.BLL.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository IemployeeRepository { get; set; } //Signature properity
        public IDepartmentRepository IdepartmentRepository { get; set; }
        int savechange();

    }
}
