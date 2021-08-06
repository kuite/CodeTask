using System.Threading.Tasks;

namespace JetShop.RentalCars.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        Task<ConfigurationSettings> GetAsync();
    }
}
