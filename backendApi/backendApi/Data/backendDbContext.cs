using backendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace backendApi.Data
{
    public class backendDbContext : DbContext
    {
        public backendDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
