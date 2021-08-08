using System;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using JetShop.RentalCars.Domain.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JetShop.RentalCars.Infrastructure.Persistence.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<CarRent> CarRents => Set<CarRent>();
        public DbSet<ConfigurationSettings> Configuration => Set<ConfigurationSettings>();

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var carCompact = new Car
            {
                Id = Guid.NewGuid(),
                Type = CarType.Compact,
                Status = CarStatus.Available
            };
            var carMinivan = new Car
            {
                Id = Guid.NewGuid(),
                Type = CarType.Minivan,
                Status = CarStatus.Available
            };
            var carPremium = new Car
            {
                Id = Guid.NewGuid(),
                Type = CarType.Premium,
                Status = CarStatus.Available
            };

            modelBuilder.Entity<Car>().HasData(carCompact);
            modelBuilder.Entity<Car>().HasData(carMinivan);
            modelBuilder.Entity<Car>().HasData(carPremium);

            var configuration = new ConfigurationSettings
            {
                Id = Guid.NewGuid(),
                ClientName = "BestClient",
                BaseDayRental = 1.4,
                KilometerPrice = 1.5
            };

            modelBuilder.Entity<ConfigurationSettings>().HasData(configuration);
        }
    }
}
