using Microsoft.EntityFrameworkCore;
using SafeBoda.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeBoda.Infrastructure
{
    public class EfTripRepository : ITripRepositoryDb
    {
        private readonly SafeBodaDbContext _dbcontext;

        public EfTripRepository(SafeBodaDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        public async Task<List<Trip>> GetActiveTripsAsync()
        {
            return await _dbcontext.Trips.ToListAsync();
        }

        public async Task<Trip?> GetTripByIdAsync(Guid tripId)
        {
            return await _dbcontext.Trips.FindAsync(tripId);
        }

        public async Task AddTripAsync(Trip trip)
        {
           
                await _dbcontext.Trips.AddAsync(trip);
                await _dbcontext.SaveChangesAsync();


            
     
        }

        public async Task<Trip?> UpdateTripAsync(Guid tripId, Trip trip)
        {
            var existing = await _dbcontext.Trips.FindAsync(tripId);
            if (existing == null) return null;

            existing.Start = trip.Start;
            existing.End = trip.End;
            existing.Fare = trip.Fare;
            existing.RiderId = trip.RiderId;
            existing.DriverId = trip.DriverId;
            existing.RequestTime = trip.RequestTime;

            await _dbcontext.SaveChangesAsync();
            return existing;
        }

        public async Task<Trip?> PatchTripAsync(Guid tripId, Trip trip)
        {
            var existing = await _dbcontext.Trips.FindAsync(tripId);
            if (existing == null) return null;

            if (trip.Fare != 0) existing.Fare = trip.Fare;
            if (trip.Start != null) existing.Start = trip.Start;
            if (trip.End != null) existing.End = trip.End;
            if (trip.RiderId != Guid.Empty) existing.RiderId = trip.RiderId;
            if (trip.DriverId != Guid.Empty) existing.DriverId = trip.DriverId;
            if (trip.RequestTime != default(DateTime)) existing.RequestTime = trip.RequestTime;

            await _dbcontext.SaveChangesAsync();
            return existing;
        }

        public async Task<Trip?> DeleteTripAsync(Guid tripId)
        {
            var existing = await _dbcontext.Trips.FindAsync(tripId);
            if (existing == null) return null;

            _dbcontext.Trips.Remove(existing);
            await _dbcontext.SaveChangesAsync();
            return existing;
        }
    }
}
