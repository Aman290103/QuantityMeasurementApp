using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
