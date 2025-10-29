using Microsoft.EntityFrameworkCore;
using SafeBoda.Core;

namespace SafeBoda.Infrastructure
{
    public class SafeBodaDbContext : DbContext
    {
        public SafeBodaDbContext(DbContextOptions<SafeBodaDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
