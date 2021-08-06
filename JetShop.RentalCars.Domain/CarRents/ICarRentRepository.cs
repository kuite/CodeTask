using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JetShop.RentalCars.Domain.CarRents
{
    public interface ICarRentRepository
    {
        Task<CarRent> GetByIdAsync(Guid id);

        Task<Guid> Create(CarRent carRent);

        Task<IEnumerable<CarRent>> GetAllAsync();

        Task<CarRent> UpdateAsync(CarRent carRent);
    }
}
