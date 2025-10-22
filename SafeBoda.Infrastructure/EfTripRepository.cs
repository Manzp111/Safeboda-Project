using Microsoft.EntityFrameworkCore;
using SafeBoda.Core;
using SafeBoda.Infrastructure;
using SafeBoda.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeBoda.Infrastructure
{
    public class EfTripRepository : ITripRepository
    {
        private readonly SafeBodaDbContext _context;

        public EfTripRepository(SafeBodaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _context.Trips
                .Include(t => t.Rider)
                .Include(t => t.Driver)
                .ToListAsync();
        }

        public async Task<Trip?> GetTripByIdAsync(Guid id)
        {
            return await _context.Trips
                .Include(t => t.Rider)
                .Include(t => t.Driver)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trip> AddTripAsync(Trip trip)
        {
           
            bool exists = await _context.Trips.AnyAsync(t =>
                t.RiderId == trip.RiderId &&
                t.DriverId == trip.DriverId &&
                t.Start.Latitude == trip.Start.Latitude &&
                t.Start.Longitude == trip.Start.Longitude &&
                t.End.Latitude == trip.End.Latitude &&
                t.End.Longitude == trip.End.Longitude &&
                t.RequestTime == trip.RequestTime
            );

            if (exists)
                throw new InvalidOperationException("A similar trip already exists.");

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip?> UpdateTripAsync(Trip trip)
        {
            var existingTrip = await _context.Trips.FindAsync(trip.Id);
            if (existingTrip == null) return null;

            _context.Entry(existingTrip).CurrentValues.SetValues(trip);
            await _context.SaveChangesAsync();
            return existingTrip;
        }

        public async Task<bool> DeleteTripAsync(Guid id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return false;

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}