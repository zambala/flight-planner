using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> GetAllAirports(string search)
        {
            search = search.ToLower().Trim();
            return _context.Airports
                .Where(a =>
                    a.AirportCode.Contains(search) ||
                    a.City.Contains(search) ||
                    a.Country.Contains(search))
                .ToList();
        }
    }
}
