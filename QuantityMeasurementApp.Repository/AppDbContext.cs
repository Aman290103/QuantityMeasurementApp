using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
