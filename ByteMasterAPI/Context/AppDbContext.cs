using ByteMasterAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ByteMasterAPI.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> clientetb { get; set; }
    }
}
