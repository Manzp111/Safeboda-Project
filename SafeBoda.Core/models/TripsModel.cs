using System;

namespace SafeBoda.Core;

public class Trip
{
    public Guid Id { get; set; }
    public Guid RiderId { get; set; }
    public Guid DriverId { get; set; }
    public Location Start { get; set; }
    public Location End { get; set; }
    public decimal Fare { get; set; }
    public DateTime RequestTime { get; set; }


}
