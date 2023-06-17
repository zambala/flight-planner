using System.Collections.Generic;

namespace FlightPlanner.API.Models
{
    public class PagedFlightSearchResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<FlightSearchResult> Items { get; set; }
    }
}
