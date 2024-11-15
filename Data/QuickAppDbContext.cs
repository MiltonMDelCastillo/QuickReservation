using Microsoft.EntityFrameworkCore;
using QuickReservation.Models;

namespace QuickReservation.Data
{
    public class QuickAppDbContext : DbContext
    {
        public QuickAppDbContext(DbContextOptions<QuickAppDbContext> options)
            : base(options)
        {
        }

        // Define tus DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
