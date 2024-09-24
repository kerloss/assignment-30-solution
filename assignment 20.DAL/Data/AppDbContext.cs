using assignment_20.DAL.Data.Configurations;
using assignment_20.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.DAL.Data
{
    public class AppDbContext : /*DbContext*/ IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=KERLOSS; DataBase=C42G02MVC; Trusted_Connection:True; MultipleActiveResultSets=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent APIS
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());  //applyCOnfiguration for all model configuration
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>()
                        .ToTable("Roles");
            modelBuilder.Entity<IdentityUser>()
                        .ToTable("Users");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public IdentityUser<int> Users { get; set; }
        //public IdentityRole<int> Roles { get; set; }
    }
}
