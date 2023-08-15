using eVidence_API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace eVidence_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=evidence;uid=evidence;pwd=3Vidence;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(a => a.Departments)
                .WithMany(a => a.Groups)
                .UsingEntity(a => a.ToTable("GroupDepartments"));
        }
    }
}
