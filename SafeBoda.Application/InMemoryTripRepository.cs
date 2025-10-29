using SafeBoda.Core;
using System.Collections.Generic;

namespace SafeBoda.Application;

public class InMemoryTripRepository : ITripRepository
{
    public IEnumerable<Trip> GetActiveTrips()
    {
        return new List<Trip>
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
}
