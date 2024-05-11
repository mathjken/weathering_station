
using MonitoringStationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MonitoringStationAPI.Database
{
    public class EnvironmentalDataContext : DbContext
    {
        public EnvironmentalDataContext(DbContextOptions<EnvironmentalDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Sensor> Sensor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=EnvironmentalData.db");

    }
}
