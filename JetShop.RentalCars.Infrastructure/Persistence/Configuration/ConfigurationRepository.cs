using System.Threading.Tasks;
using JetShop.RentalCars.Domain.Configuration;
using JetShop.RentalCars.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace JetShop.RentalCars.Infrastructure.Persistence.Configuration
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly DatabaseContext _context;

        public ConfigurationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ConfigurationSettings> GetAsync()
        {
            return await _context.Configuration
                .FirstOrDefaultAsync();
        }
    }
}
