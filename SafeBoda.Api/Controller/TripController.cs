using Microsoft.AspNetCore.Mvc;
using SafeBoda.Core;
using SafeBoda.Infrastructure;

namespace SafeBoda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trips = await _tripRepository.GetAllTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);
            if (trip == null) return NotFound();
            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trip trip)
        {
            try
            
            {
                var created = await _tripRepository.AddTripAsync(trip);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Trip trip)
        {
            if (id != trip.Id) return BadRequest("Trip ID mismatch");

            var updated = await _tripRepository.UpdateTripAsync(trip);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _tripRepository.DeleteTripAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}