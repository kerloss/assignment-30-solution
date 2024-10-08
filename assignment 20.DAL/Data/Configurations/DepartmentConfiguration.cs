﻿using assignment_20.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIS
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.HasMany(E => E.employees)
                   .WithOne(D => D.departments)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(E => E.Code).IsRequired(true);
            builder.Property(E => E.Name).IsRequired(true);
            //builder.Property(E => E.DateOfCreation).HasColumnName("Date Of Creation");
        }
    }
}
