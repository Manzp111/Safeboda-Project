using Microsoft.EntityFrameworkCore;
using SafeBoda.Core;

namespace SafeBoda.Infrastructure
{
    public class SafeBodaDbContext : DbContext
    {
        public SafeBodaDbContext(DbContextOptions<SafeBodaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fix decimal precision
            modelBuilder.Entity<Trip>()
                .Property(t => t.Fare)
                .HasPrecision(18, 2);

            // Prevent cascade delete (the cause of your error)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Start)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.End)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}