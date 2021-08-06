using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.Cars;
using MediatR;

namespace JetShop.RentalCars.Application.Cars.GetAllCarDetails
{
    public class GetAllCarDetailsQueryHandler : IRequestHandler<GetAllCarDetailsQuery, IEnumerable<CarDetailsDto>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarDetailsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<CarDetailsDto>> Handle(GetAllCarDetailsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllAsync();
            return cars.Select(x => 
                new CarDetailsDto
                {
                    Id = x.Id,
                    CarType = x.Type.ToString(),
                    Status = x.Status.ToString(),
                    Mileage = x.Mileage
                });
        }
    }
}
