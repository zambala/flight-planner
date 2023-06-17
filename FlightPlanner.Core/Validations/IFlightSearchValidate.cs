using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public interface IFlightSearchValidate
    {
        bool IsValid(FlightSearch search);
    }
}
