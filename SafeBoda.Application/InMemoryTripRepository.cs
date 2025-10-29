using SafeBoda.Core;
using System.Collections.Generic;
using System.Linq;

namespace SafeBoda.Application;




public class InMemoryTripRepository : ITripRepository
{
    
public List<Trip> GetActiveTrips()
    {
        var l1Stsrt = new Location(30, 70);
        var l1end = new Location(40, 80);
        var l2Stsrt = new Location(20, 50);
        var l2end = new Location(10, 80);
     var t1=new Trip(Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),l1Stsrt,l1end,600,requestTime: DateTime.Now);
     var t2=new Trip(Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),l2Stsrt,l2end,700,requestTime: DateTime.Now);

    List<Trip> trips = new List<Trip>{t1,t2};
        
        
        return trips;
    }

     
    

}
