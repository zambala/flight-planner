using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        private readonly ICleanupService _cleanupService;
        public CleanupApiController(ICleanupService cleanupService)
        {
            _cleanupService = cleanupService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _cleanupService.CleanupDatabase();

            return Ok();
        }
    }
}