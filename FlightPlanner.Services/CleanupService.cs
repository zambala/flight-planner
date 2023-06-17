using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class CleanupService : ICleanupService
    {
        private readonly IFlightPlannerDbContext _context;

        public CleanupService(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        public void CleanupDatabase()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}
