using JetShop.RentalCars.Domain.SeedWork;

namespace JetShop.RentalCars.Domain.Configuration
{
    public class ConfigurationSettings : Entity
    {
        public string ClientName { get; set; }

        public double BaseDayRental { get; set; }

        public double KilometerPrice { get; set; }
    }
}
