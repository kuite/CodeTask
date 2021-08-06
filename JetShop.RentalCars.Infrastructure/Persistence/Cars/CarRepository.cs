using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.Cars;
using JetShop.RentalCars.Domain.Configuration;
using JetShop.RentalCars.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace JetShop.RentalCars.Infrastructure.Persistence.Cars
{
    public class CarRepository : ICarRepository
    {
        private readonly DatabaseContext _context;

        public CarRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Car> GetByIdAsync(Guid id)
        {
            var car = await _context.Cars
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return car;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }
    }
}
