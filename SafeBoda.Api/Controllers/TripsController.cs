using Microsoft.AspNetCore.Mvc;
using SafeBoda.Core;
using SafeBoda.Application;

namespace SafeBoda.Api.Controllers
{
    [ApiController]
    [Route("trip")]
    
    public class Trips : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public Trips(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        // GET: api/trips/list/
        [HttpGet("list")]
        public IActionResult GetAllActiveTrips()
        {
            var trips= _tripRepository.GetActiveTrips();
            return Ok(trips);
        }
        
        // Post :api/trips/add
        [HttpPost("add")]
        public IActionResult CreateTripRequest([FromBody] TripRequest request)
        {
            // Create new trip using the record constructor
            var newTrip = new Trip(
                Id: Guid.NewGuid(),
                RiderId: request.RiderId,
                DriverId: Guid.Empty, // Unassigned driver
                Start: new Location(request.StartLatitude, request.StartLongitude),
                End: new Location(request.EndLatitude, request.EndLongitude),
                Fare: 0, // Simulate fare calculation
                RequestTime: DateTime.UtcNow
            );

            
            return CreatedAtAction(nameof(GetAllActiveTrips), new { id = newTrip.Id }, newTrip);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetTripById(Guid id)
        {
            var trip = _tripRepository.GetTripById(id);
            if (trip == null)
                return NotFound($"Trip with ID {id} not found.");

            return Ok(trip);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrip(Guid id)
        {
            var trip = _tripRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound($"Trip with ID {id} not found.");
            }

            return NoContent();


        }


    }
}



    
    
