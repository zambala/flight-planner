using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.API.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IFlightSearchValidate _flightSearchValidator;

        public CustomerApiController(
            ICustomerService customerService,
            IFlightSearchValidate flightSearchValidator,
            IMapper mapper)
        {
            _customerService = customerService;
            _flightSearchValidator = flightSearchValidator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            var result = _customerService.GetAllAirports(search);

            if (result == null) return NotFound();

            List<AirportSearchResult> searchResult = new List<AirportSearchResult>();

            foreach (var airport in result)
            {
                searchResult.Add(new AirportSearchResult
                {
                    Airport = airport.AirportCode,
                    City = airport.City,
                    Country = airport.Country
                });
            }

            return Ok(searchResult);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(FlightSearch searchFlight)
        {
            if (_flightSearchValidator.IsValid(searchFlight)) return BadRequest();

            var flights = _customerService.FindFlights(searchFlight);

            PagedFlightSearchResult searchResult = _mapper.Map<PagedFlightSearchResult>(flights);

            return Ok(searchResult);
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _customerService.GetFullFlight(id);

            if (flight == null) return NotFound();

            var flightResult = _mapper.Map<FlightSearchResult>(flight);

            return Ok(flightResult);
        }
    }
}