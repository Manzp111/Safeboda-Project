using SafeBoda.Core;
using System.Collections.Generic;

namespace SafeBoda.Application;

public interface ITripRepository
{
    List<Trip> GetActiveTrips();

    


}

