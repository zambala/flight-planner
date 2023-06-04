
using System.Threading;
using Flight_plannerAPI.Models;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{

    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest flight)
        {
            if (ValidateFlight.HasInvalidFlightDetails(flight) ||
                ValidateFlight.HasInvalidAirport(flight) ||
                ValidateFlight.HasInvalidFlightTime(flight))
                return BadRequest();

            if (FlightStorage.FlightExists(flight)) return Conflict();

            return Created("", FlightStorage.AddFlight(flight));
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
            
        }
    }
}