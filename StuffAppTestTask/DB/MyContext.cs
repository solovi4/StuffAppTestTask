using Microsoft.EntityFrameworkCore;
using StuffAppTestTask.Models;

namespace StuffAppTestTask.DB
{
    public class MyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgramLanguage> ProgramLanguages { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experience>().HasKey(e => new 
            { 
                e.EmployeeId,
                e.ProgramLanguageId
            });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=employeedb;Username=postgres;Password=111");
        }
    }
}