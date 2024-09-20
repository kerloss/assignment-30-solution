using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.DAL.Models
{
    // model [class represent table in DataBase]
    public class Department : ModelBase
    {
        //[Required(ErrorMessage ="Code is required!")]
        public string Code { get; set; }    //.Net 5 allow null
        //[Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        //[Display(Name ="Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //Navigation Property [Many]
        //[InverseProperty(nameof(Models.Employee.departments))]
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
    }
}
