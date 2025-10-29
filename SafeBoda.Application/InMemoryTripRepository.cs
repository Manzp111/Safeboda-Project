// using SafeBoda.Core;
// using System.Collections.Generic;
// using System.Linq;

// namespace SafeBoda.Application;




// public class InMemoryTripRepository : ITripRepository
// {
//     private readonly List<Trip> _trips = new List<Trip>();
//         public InMemoryTripRepository()
//         {
//             var l1Start = new Location(30, 70);
//             var l1End = new Location(40, 80);
//         var t1 = new Trip(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), l1Start, l1End, 600, DateTime.Now);
//         var l2Start = new Location(35, 75);
//         var l2End = new Location(45, 85);
//         var t2 = new Trip(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), l2Start, l2End, 800, DateTime.Now);
//         _trips.Add(t1);
//         _trips.Add(t2);
//         }

// public List<Trip> GetActiveTrips()
//     {
//         return _trips;

//     }

//     public  Task AddTrip(Trip trip)
//     {
//         var existingTrip =_trips.Find(t => t.Id == trip.Id);
//         if (existingTrip != null)
//             _trips.Add(trip);
//         return Task.CompletedTask;



//     }
//     public Task<Trip?> DeleteTrip(Guid tripId)
//     {

//         var trip = _trips.FirstOrDefault(t => t.Id == tripId);
//         if (trip != null)
//         {
//             _trips.Remove(trip);
//         }


//         return Task.FromResult(trip);

//     }

//     public Task<Trip?> GetTripById(Guid tripId)
//     {
//         var trip = _trips.FirstOrDefault(t => t.Id == tripId);
//         return Task.FromResult(trip);
//     }

//     public Task<Trip?> UpdateTrip(Trip trip, Guid tripId)
//     {
//         var existingTrip = _trips.FirstOrDefault(t => t.Id == tripId);
//         if (existingTrip != null)
//         {
//             existingTrip.RiderId = trip.RiderId;
//             existingTrip.DriverId = trip.DriverId;
//             existingTrip.Start = trip.Start;
//             existingTrip.End = trip.End;
//             existingTrip.Fare = trip.Fare;
//             existingTrip.RequestTime = trip.RequestTime;

//             return Task.FromResult<Trip?>(existingTrip);
//         }
//         else
//         {
//             throw new Exception("Trip not found");
//         }
//         // return Task.FromResult(existingTrip!);
//     }

//     public Task<Trip?> PatchTrip(Guid tripId, Trip trip)
//     {
//         var existingTrip = _trips.FirstOrDefault(t => t.Id == tripId);
//         if (existingTrip != null)
//         {
//             if (trip.RiderId != Guid.Empty)
//                 existingTrip.RiderId = trip.RiderId;
//             if (trip.DriverId != Guid.Empty)
//                 existingTrip.DriverId = trip.DriverId;
//             if (trip.Start != null)
//                 existingTrip.Start = trip.Start;
//             if (trip.End != null)
//                 existingTrip.End = trip.End;
//             if (trip.Fare != 0)
//                 existingTrip.Fare = trip.Fare;
//             if (trip.RequestTime != default(DateTime))
//                 existingTrip.RequestTime = trip.RequestTime;

//             return Task.FromResult<Trip?>(existingTrip);




//         }
//         else
//         {
//             throw new Exception("Trip not found");
//         }
//         // return Task.FromResult(existingTrip!);
//     }
    
    




// }
