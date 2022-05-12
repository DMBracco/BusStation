using BusStation.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusStation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Platform> Platforms => Set<Platform>();
        public DbSet<Entities.Route> Routes => Set<Entities.Route>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<BusStop> BusStops => Set<BusStop>();
        public DbSet<Bus> Buses => Set<Bus>();
        public DbSet<BusSchedule> BusSchedules => Set<BusSchedule>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<RouteDay> RouteDays => Set<RouteDay>();
        public DbSet<StoOfBus> StoOfBuses => Set<StoOfBus>();
    }
}