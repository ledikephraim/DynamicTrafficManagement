using ManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementAPI.Data.DAL
{
    public class TrafficManagementContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Intersection> Intersections { get; set; }

        public TrafficManagementContext(DbContextOptions<TrafficManagementContext> options) : base(options)
        {

        }
    }
}
