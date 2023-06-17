using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Services.Validations
{
    public class FlightSearchValidator : IFlightSearchValidate
    {
        public bool IsValid(FlightSearch search)
        {
            return string.IsNullOrEmpty(search.To) || 
                   string.IsNullOrEmpty(search.From) || 
                   search.To == search.From;
        }
    }
}
