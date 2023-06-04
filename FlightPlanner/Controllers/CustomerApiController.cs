using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            var result = FlightStorage.FindAirport(search);
            return Ok(result);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(FlightSearch searchFlight)
        {
            if (ValidateSearch.HasInvalidDetails(searchFlight) ||
                ValidateSearch.MatchingAirport(searchFlight))
                return BadRequest();


            var result = FlightStorage.SearchFlight(searchFlight);
            return Ok(result);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}