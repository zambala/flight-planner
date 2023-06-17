using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);
        public bool FlightExists(Flight flight);
        List<Flight> SearchFlights(FlightSearch request);
    }
}
