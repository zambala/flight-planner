using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;

        public CustomerService(IAirportService airportService,
            IFlightService flightService)
        {
            _airportService = airportService;
            _flightService = flightService;
        }

        public PageResult FindFlights(FlightSearch search)
        {
            var flights = _flightService.SearchFlights(search);
            PageResult result = new PageResult
            {
                TotalItems = flights.Count,
                Items = flights.ToList(),
                Page = 0
            };
            return result;
        }

        public Flight GetFullFlight(int id)
        {
            return _flightService.GetFullFlight(id);
        }

        public List<Airport> GetAllAirports(string search)
        {
            return _airportService.GetAllAirports(search);
        }
    }
}
