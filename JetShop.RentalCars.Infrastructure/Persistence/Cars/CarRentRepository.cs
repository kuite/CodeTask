using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace JetShop.RentalCars.Infrastructure.Persistence.Cars
{
    public class CarRentRepository : ICarRentRepository
    {
        private readonly DatabaseContext _context;

        public CarRentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<CarRent> GetByIdAsync(Guid id)
        {
            var carRent = await _context.CarRents
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return carRent;
        }

        public async Task<Guid> Create(CarRent carRent)
        {
            var entity = await _context.CarRents.AddAsync(carRent);
            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<IEnumerable<CarRent>> GetAllAsync()
        {
            return await _context.CarRents.ToListAsync();
        }

        public async Task<CarRent> UpdateAsync(CarRent carRent)
        {
            _context.CarRents.Update(carRent);
            await _context.SaveChangesAsync();
            return carRent;
        }
    }
}
