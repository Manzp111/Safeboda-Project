using SafeBoda.Core;
using System.Collections.Generic;
using System.Linq;

namespace SafeBoda.Application;




public class InMemoryTripRepository : ITripRepository
{
    private readonly List<Trip> _trips = new List<Trip>();
        public InMemoryTripRepository()
        {
            var l1Start = new Location(30, 70);
            var l1End = new Location(40, 80);
            var t1 = new Trip(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), l1Start, l1End, 600, DateTime.Now);
            _trips.Add(t1);
        }

public List<Trip> GetActiveTrips()
    {
        return _trips;

    }

 public Task AddTrip(Trip trip)
    {
        _trips.Add(trip);
        return Task.CompletedTask;

        
        
    }



}
