using System;
using FlightPlanner.Models;

namespace FlightPlanner.Validate
{
    public class ValidateFlight
    {
        public static bool HasInvalidFlightDetails(AddFlightRequest flight)
        {
            if (flight == null || flight.From == null || flight.To == null) return true;

            return string.IsNullOrWhiteSpace(flight.From.AirportCode) ||
                   string.IsNullOrWhiteSpace(flight.To.AirportCode) ||
                   string.IsNullOrWhiteSpace(flight.From.Country) ||
                   string.IsNullOrWhiteSpace(flight.To.Country) ||
                   string.IsNullOrWhiteSpace(flight.From.City) ||
                   string.IsNullOrWhiteSpace(flight.To.City) ||
                   string.IsNullOrWhiteSpace(flight.Carrier) ||
                   string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                   string.IsNullOrWhiteSpace(flight.ArrivalTime);
        }
        public static bool HasInvalidFlightTime(AddFlightRequest flight)
        {
            if (DateTime.TryParse(flight.ArrivalTime, out var arrivalTime) &&
                DateTime.TryParse(flight.DepartureTime, out var departureTime))
                return arrivalTime <= departureTime;

            return false;
        }
        public static bool HasInvalidAirport(AddFlightRequest flight)
        {
            return flight.From.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper();
        }
    }
}
