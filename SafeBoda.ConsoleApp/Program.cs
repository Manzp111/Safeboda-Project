using SafeBoda.Application;

var repo = new InMemoryTripRepository();
var trips = repo.GetActiveTrips();

foreach (var trip in trips)
{
    Console.WriteLine($"Trip ID: {trip.Id}| driver id{trip.DriverId} | Fare: {trip.Fare} | From: ({trip.Start.Latitude},{trip.Start.Longitude}) to ({trip.End.Latitude},{trip.End.Longitude})");
}
