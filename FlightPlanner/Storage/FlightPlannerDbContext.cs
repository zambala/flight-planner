using Microsoft.EntityFrameworkCore;
using Flight_plannerAPI.Models;

namespace FlightPlanner.Storage
{
    public class FlightPlannerDbContext: DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {
            
        }
        public DbSet <Flight> Flights{ get; set; }
        public DbSet <Airport> Airports { get; set; }
    }
}
