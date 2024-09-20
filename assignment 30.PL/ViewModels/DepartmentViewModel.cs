using assignment_20.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace assignment_30.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required!")]
        public string Code { get; set; }    //.Net 5 allow null
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //Navigation Property [Many]
        [InverseProperty(nameof(assignment_20.DAL.Models.Employee.departments))]
        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
    }
}
