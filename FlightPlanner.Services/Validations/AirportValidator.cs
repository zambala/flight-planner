using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Services.Validations
{
    public class AirportValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.From != null && flight?.To != null;
        }
    }
}
