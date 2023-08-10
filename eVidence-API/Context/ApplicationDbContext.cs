using eVidence_API.Models;
using Microsoft.EntityFrameworkCore;

namespace eVidence_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=evidence;uid=evidence;pwd=3Vidence;");
        }
    }
}
