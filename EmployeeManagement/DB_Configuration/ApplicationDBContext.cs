using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;

namespace EmployeeManagement.DB_Configuration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<User> users { get; set; }
    }
}
