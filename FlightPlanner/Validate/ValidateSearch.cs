using FlightPlanner.Models;

namespace FlightPlanner.Validate
{
    public class ValidateSearch
    {
        public static bool HasInvalidDetails(FlightSearch searchFlight)
        {
            return searchFlight.From == null ||
                   searchFlight.To == null ||
                   searchFlight.DepartureDate == null;
        }
        public static bool MatchingAirport(FlightSearch searchFlight)
        {
            return searchFlight.From == searchFlight.To;
        }
    }
}
