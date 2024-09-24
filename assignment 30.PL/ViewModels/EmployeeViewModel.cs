using assignment_20.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace assignment_30.PL.ViewModels
{
    public enum Gender //default acces in internal
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }   

    enum EmployeeType
    {
        FullTime = 1,
        PartTime = 2
    }
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        [MinLength(4, ErrorMessage = "Minimum Name Length is 4 charactar")]
        [MaxLength(50, ErrorMessage = "Maximum Name Length is 4 charactar")]
        public string Name { get; set; }

        [Range(21, 60)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; } //2 type of delete =>1. hard deleted that delete from dataBase, 2. soft delete that delete but not deleted from DB

        public Gender Gender { get; set; }

        //Navigation Property [one]
        [InverseProperty(nameof(assignment_20.DAL.Models.Department.employees))]
        public Department departments { get; set; }
        public int? DepartmentId { get; set; }  //FK Colomn //Default for delete in NoAction
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
    }
}
