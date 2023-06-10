using System;
using System.Linq;
using Flight_plannerAPI.Models;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{

    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private static readonly object _flightLock = new object();
        public AdminApiController(FlightPlannerDbContext context) : base(context)
        {
        }
        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
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

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest flight)
        {
            if (flight==null)return BadRequest();

            if (ValidateFlight.HasInvalidFlightDetails(flight) ||
                ValidateFlight.HasInvalidAirport(flight) ||
                ValidateFlight.HasInvalidFlightTime(flight))
                return BadRequest();


            Flight newFlight = null;

            lock(_flightLock)
            {
                var existingFlight = _context.Flights
                    .Any(f => f.From.AirportCode == flight.From.AirportCode &&
                              f.To.AirportCode == flight.To.AirportCode &&
                              f.Carrier == flight.Carrier &&
                              f.DepartureTime == flight.DepartureTime &&
                              f.ArrivalTime == flight.ArrivalTime);

                if(existingFlight)return Conflict();

                newFlight = new Flight
                {
                    From = flight.From,
                    To = flight.To,
                    Carrier = flight.Carrier,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime
                };

                _context.Flights.Add(newFlight);
                _context.SaveChanges();

            }
            return Created("", newFlight);
        }

        [HttpDelete]
        [Route("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
            
        }
    }
}