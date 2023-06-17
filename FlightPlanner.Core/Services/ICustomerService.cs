using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface ICustomerService
    {
        PageResult FindFlights(FlightSearch search);
        Flight GetFullFlight(int id);
        List<Airport> GetAllAirports(string search);
    }
}
