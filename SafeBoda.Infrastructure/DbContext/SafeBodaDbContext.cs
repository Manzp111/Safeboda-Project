using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SafeBoda.Core;

namespace SafeBoda.Infrastructure
{
    public  sealed class SafeBodaDbContext : IdentityDbContext<User> //avoid inheriting from DbContext for security reasons
    {
        public SafeBodaDbContext(DbContextOptions<SafeBodaDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Location> Locations { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Trip>(trip =>
    {
        trip.OwnsOne(t => t.Start);
        trip.OwnsOne(t => t.End);

        trip.Property(t => t.Fare)
            .HasColumnType("decimal(18,2)");
    });
}

    }

}
