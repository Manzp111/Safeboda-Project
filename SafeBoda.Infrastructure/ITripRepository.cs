using SafeBoda.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeBoda.Infrastructure
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip?> GetTripByIdAsync(Guid id);
        Task<Trip> AddTripAsync(Trip trip);
        Task<Trip?> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(Guid id);
    }
}