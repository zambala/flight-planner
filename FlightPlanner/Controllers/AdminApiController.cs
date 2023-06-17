using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.API.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private static readonly object _lock = new object();
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidate> _validators;

        public AdminApiController(
            IFlightService flightService,
            IMapper mapper,
            IEnumerable<IValidate> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight == null) return NotFound();

            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock (_lock)
            {
                var flight = _mapper.Map<Flight>(request);

                if (!_validators.All(v => v.IsValid(flight))) return BadRequest();

                if (_flightService.FlightExists(flight)) return Conflict();

                _flightService.Create(flight);

                return Created("", _mapper.Map<AddFlightRequest>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight == null) return Ok();

            _flightService.Delete(flight);

            return Ok();
        }
    }
}