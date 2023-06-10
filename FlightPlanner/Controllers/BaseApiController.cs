using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected FlightPlannerDbContext _context;
        public BaseApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
    }
}
