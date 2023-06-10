using System.Linq;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : BaseApiController
    {
        public CustomerApiController(FlightPlannerDbContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            if(string.IsNullOrEmpty(search)) return BadRequest();

            var lowerSearch = search.Trim().ToLower();

            var result = _context.Airports
                .Where(a => a.AirportCode.Contains(lowerSearch) ||
                            a.City.Contains(lowerSearch) ||
                            a.Country.Contains(lowerSearch))
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(FlightSearch searchFlight)
        {
            if (ValidateSearch.HasInvalidDetails(searchFlight) ||
                ValidateSearch.MatchingAirport(searchFlight))
                return BadRequest();


            var result = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .Where(f => f.From.AirportCode == searchFlight.From &&
                            f.To.AirportCode == searchFlight.To &&
                            f.DepartureTime.StartsWith(searchFlight.DepartureDate))
                .ToList();

            var response = new
            {
                page = 0,
                totalItems = result.Count,
                items = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}