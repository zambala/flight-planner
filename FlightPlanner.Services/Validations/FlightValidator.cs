using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Services.Validations
{
    public class FlightValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight != null;
        }
    }
}
