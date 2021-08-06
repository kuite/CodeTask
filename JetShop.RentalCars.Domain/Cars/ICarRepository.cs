using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JetShop.RentalCars.Domain.Cars
{
    public interface ICarRepository
    {
        Task<Car> GetByIdAsync(Guid id);

        Task<Car> UpdateAsync(Car car);

        Task<IEnumerable<Car>> GetAllAsync();
    }
}
