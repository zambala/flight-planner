using FlightPlanner.Core.Models;

namespace FlightPlanner.API.Models
{
    public class FlightSearchResult : Entity
    {
        public AirportSearchResult From { get; set; }
        public AirportSearchResult To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
