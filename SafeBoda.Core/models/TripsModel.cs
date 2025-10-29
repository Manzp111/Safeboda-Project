using System;

namespace SafeBoda.Core;

public class Trip
{
    public Guid Id { get; set; }=Guid.NewGuid();
    public Guid RiderId { get; set; }
    public Guid DriverId { get; set; }
    public Location Start { get; set; }
    public Location End { get; set; }
    public decimal Fare { get; set; }
    public DateTime RequestTime { get; set; }

    public Trip(Guid id, Guid riderId, Guid driverId, Location start, Location end, decimal fare, DateTime requestTime)
    {
        Id = id;
        RiderId = riderId;
        DriverId = driverId;
        Start = start;
        End = end;
        Fare = fare;
        RequestTime = requestTime;
    }
}
