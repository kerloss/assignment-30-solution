using assignment_20.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.DAL.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Fluent API
            builder.Property(E=>E.Salary).HasColumnType("decimal(18,2)");

            //HasConversion take 2 paramters first one that property saved in DB then it save with string
            //second that when take gender from DB u get it like string and convert it to Gender
            builder.Property(E => E.Gender).HasConversion(
                (Gender) => Gender.ToString(),
                (genderAsString => (Gender)Enum.Parse(typeof(Gender), genderAsString, true))
                );

            builder.Property(E => E.Name)
                   .IsRequired(true)
                   .HasMaxLength(50);

            //builder.Property(E => E.PhoneNumber).HasColumnName("Phone Number");
            //builder.Property(E => E.HireDate).HasColumnName("Hire Date");
        }
    }
}
