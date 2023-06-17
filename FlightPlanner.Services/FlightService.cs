using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool FlightExists(Flight flight)
        {
            return _context.Flights.Any(f =>
                f.ArrivalTime == flight.ArrivalTime &&
                f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.From.City == flight.From.City &&
                f.From.Country == flight.From.Country &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.To.City == flight.To.City &&
                f.To.Country == flight.To.Country
            );
        }

        public List<Flight> SearchFlights(FlightSearch request)
        {
            return _context.Flights
                .Where(f => f.To.AirportCode == request.To && f.From.AirportCode == request.From)
                .ToList();
        }
    }
}
