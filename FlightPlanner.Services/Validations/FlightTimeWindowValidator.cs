using System;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Services.Validations
{
    public class FlightTimeWindowValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return DateTime.TryParse(flight?.DepartureTime, out var departureTime) &&
                   DateTime.TryParse(flight?.ArrivalTime, out var arrivalTime) &&
                   departureTime < arrivalTime;
        }
    }
}
