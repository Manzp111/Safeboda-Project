using SafeBoda.Core;
using System.Collections.Generic;
using System.Linq;

namespace SafeBoda.Application;

public class InMemoryTripRepository : ITripRepository
{
    // Store trips in a list so we can query by ID
    private readonly List<Trip> _trips;

    public InMemoryTripRepository()
    {
        _trips = new List<Trip>
        {
            new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Location(-1.95, 30.09),
                new Location(-1.94, 30.10),
                1500m,
                DateTime.Now
            ),
            new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Location(-1.94, 30.10),
                new Location(-1.93, 30.12),
                2000m,
                DateTime.Now.AddMinutes(-10)
            )
        };
    }

    public IEnumerable<Trip> GetActiveTrips() => _trips;

    
    public Trip? GetTripById(Guid id) => _trips.FirstOrDefault(t => t.Id == id);

    
    public void AddTrip(Trip  trip) => _trips.Add(trip);
    
    public void DeleteTrip(Trip trip) => _trips.Remove(trip);
}