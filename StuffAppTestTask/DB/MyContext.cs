using Microsoft.EntityFrameworkCore;

namespace StuffAppTestTask.DB
{
    public class MyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgramLanguage> ProgramLanguages { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Employee;Username=postgres;Password=111");
        }
    }
}