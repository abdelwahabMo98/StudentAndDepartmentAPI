using Lab1Sol.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1Sol.Entity
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
            
        }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
