using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SafeBoda.Application;
using SafeBoda.Core;


namespace SafeBodaContoller;


[ApiController]
[Route("trip")]
public class TripController : ControllerBase
{


    // ITripRepository name=new ITripRepository();
    private readonly ITripRepository _trips;
    
    public TripController()
    {
        _trips=new InMemoryTripRepository();
        
    }


    [HttpGet("list")]
    public IActionResult Trips()
    {
        var triplist = _trips.GetActiveTrips();
        return Ok(triplist);
    }

    [HttpPost("add")]
    public IActionResult CreateTrip([FromBody] Trip trip)
    {
    var existingTrip = _trips.GetActiveTrips().FirstOrDefault(t => t.Id == trip.Id);
    if (existingTrip != null)
    {
        return BadRequest(new
        {
            status = false,
            message = $"Trip with ID {trip.Id} already exists",
            data = (object?)null
        });
    }

        _trips.AddTrip(trip);
        return Ok(new
        {
            status=true,
            message="trip created successfully",
            data=trip,
                
    }
        
        );}
        
    
 



}

