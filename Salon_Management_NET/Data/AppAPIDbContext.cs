using Microsoft.EntityFrameworkCore;
using Salon_Management_NET.Model;

namespace Salon_Management_NET.Data
{
    public class AppAPIDbContext : DbContext
    {
        public AppAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
