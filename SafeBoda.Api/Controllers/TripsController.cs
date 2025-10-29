using Microsoft.AspNetCore.Mvc;
using SafeBoda.Application;
using SafeBoda.Core;

namespace SafeBoda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripRepository _tripRepository;

    public TripController(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    [HttpGet]
    public IActionResult GetAllActiveTrips() => Ok(_tripRepository.GetActiveTrips());

    [HttpPost("add")]
    public IActionResult CreateTrip([FromBody] TripRequest request)
    {
        var newTrip = new Trip(
            Id: Guid.NewGuid(),
            RiderId: request.RiderId,
            DriverId: Guid.Empty,
            Start: new Location(request.StartLatitude, request.StartLongitude),
            End: new Location(request.EndLatitude, request.EndLongitude),
            Fare: 0,
            RequestTime: DateTime.UtcNow
        );

        _tripRepository.AddTrip(newTrip);

        return CreatedAtAction(nameof(GetAllActiveTrips), new { id = newTrip.Id }, newTrip);
    }
}

public class TripRequest
{
    public Guid RiderId { get; set; }
    public double StartLatitude { get; set; }
    public double StartLongitude { get; set; }
    public double EndLatitude { get; set; }
    public double EndLongitude { get; set; }
}