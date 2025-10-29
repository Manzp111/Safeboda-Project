using SafeBoda.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeBoda.Infrastructure
{
    public interface ITripRepositoryDb
    {
        Task<List<Trip>> GetActiveTripsAsync();
        Task<Trip?> GetTripByIdAsync(Guid tripId);
        Task AddTripAsync(Trip trip);
        Task<Trip?> UpdateTripAsync(Guid tripId, Trip trip);
        Task<Trip?> PatchTripAsync(Guid tripId, Trip trip);
        Task<Trip?> DeleteTripAsync(Guid tripId);
    }
}
