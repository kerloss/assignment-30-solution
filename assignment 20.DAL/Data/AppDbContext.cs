using assignment_20.DAL.Data.Configurations;
using assignment_20.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_20.DAL.Data
{
    public class AppDbContext : DbContext
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
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
