using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Services.Validations
{
    public class MatchingAirportsValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.To.AirportCode.ToLower().Trim() != flight?.From.AirportCode.ToLower().Trim();
        }
    }
}
