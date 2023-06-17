using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService
    {
        List<Airport> GetAllAirports(string search);
    }
}
