using Microsoft.AspNetCore.Mvc;
using SafeBoda.Core;
using SafeBoda.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SafeBoda.Api.Controllers
{
    [ApiController]
    [Route("trip")]
    public class TripController : ControllerBase
    {
        private readonly ITripRepositoryDb _tripRepo;

        public TripController(ITripRepositoryDb tripRepo)
        {
            _tripRepo = tripRepo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripRepo.GetActiveTripsAsync();
            return Ok(new
            {
                success = true,
                message = "Trips retrieved successfully",
                data = new
                {
                    number_of_trips = trips.Count,
                    trip = trips
                }
            });
        }

        [HttpGet("detail{id}")]
        public async Task<IActionResult> GetTrip(Guid id)
        {
            var trip = await _tripRepo.GetTripByIdAsync(id);
            if (trip == null) return NotFound();
            return Ok(new
            {
                success = true,
                message = "Trip retrieved successfully",
                data = trip
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] Trip trip)
        {
            await _tripRepo.AddTripAsync(trip);
            return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip);
        }

        [HttpPut("update{id}")] // Update entire trip
        public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] Trip trip)
        {
            var updated = await _tripRepo.UpdateTripAsync(id, trip);
            if (updated == null) return NotFound();
            return Ok(new {
                success = true,
                message = "Trip updated successfully",
                data = updated
            });
        }

        [HttpPatch("partialUpdate{id}")] // Partial update of trip
        public async Task<IActionResult> PatchTrip(Guid id, [FromBody] Trip trip)
        {
            var patched = await _tripRepo.PatchTripAsync(id, trip);
            if (patched == null) return NotFound();
            return Ok(new {
                success = true,
                message = "Trip partially updated successfully",
                data = patched
            });
        }

        [HttpDelete("delete{id}")] // Delete trip
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            var deleted = await _tripRepo.DeleteTripAsync(id);
            if (deleted == null) return NotFound();
            return Ok(new {
                success = true,
                message = "Trip deleted successfully",
                
            });
        }
    }
}
