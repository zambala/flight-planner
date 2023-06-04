using System;
using System.Collections.Generic;
using System.Linq;
using Flight_plannerAPI.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;
        private static readonly object _flightLock = new();


        public static Flight GetFlight(int id)
        {
            lock (_flightLock)
            {
                return _flights.SingleOrDefault(f => f.Id == id);
            }
        }

        public static Flight AddFlight(AddFlightRequest input)
        {
            lock (_flightLock)
            {
                if (FlightExists(input)) return null;

                var flight = new Flight
                {
                    Id = _id++,
                    ArrivalTime = input.ArrivalTime,
                    Carrier = input.Carrier,
                    DepartureTime = input.DepartureTime,
                    From = input.From,
                    To = input.To
                };

                _flights.Add(flight);
                return flight;
            }
        }

        public static bool FlightExists(AddFlightRequest flight)
        {
            lock (_flightLock)
            {
                return _flights.Any(f => f.ArrivalTime == flight.ArrivalTime &&
                                         f.DepartureTime == flight.DepartureTime &&
                                         string.Equals(f.From.AirportCode, flight.From.AirportCode,
                                             StringComparison.CurrentCultureIgnoreCase) &&
                                         string.Equals(f.To.AirportCode, flight.To.AirportCode,
                                             StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 1;
        }

        public static void DeleteFlight(int id)
        {
            var flight = GetFlight(id);
            lock (_flightLock)
            {
                if (flight != null) _flights.Remove(flight);
            }
        }

        public static PageResult SearchFlight(FlightSearch flight)
        {
            var result = new PageResult();
            var flights = _flights.Where(f =>
                f.DepartureTime.Contains(flight.DepartureDate) &&
                f.From.AirportCode.StartsWith(flight.From, StringComparison.CurrentCultureIgnoreCase) &&
                f.To.AirportCode.StartsWith(flight.To, StringComparison.CurrentCultureIgnoreCase)).ToList();
            result.Items = flights;
            result.TotalItems = flights.Count;
            result.Page = 0;
            return result;
        }

        public static List<Airport> FindAirport(string searchTerm)
        {
            var matchingAirports = new List<Airport>();

            var trimmedSearchTerm = searchTerm.Trim();

            foreach (var flight in _flights)
            {
                if (AirportMatchesSearchTerm(flight.From, trimmedSearchTerm)) matchingAirports.Add(flight.From);

                if (AirportMatchesSearchTerm(flight.To, trimmedSearchTerm)) matchingAirports.Add(flight.To);
            }

            return matchingAirports;
        }

        private static bool AirportMatchesSearchTerm(Airport airport, string searchTerm)
        {
            return airport.City.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                   airport.Country.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                   airport.AirportCode.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase);
        }

    }
    }