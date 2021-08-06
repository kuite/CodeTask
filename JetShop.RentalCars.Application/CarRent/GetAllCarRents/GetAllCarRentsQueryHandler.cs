using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Configuration;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.GetAllCarRents
{
    public class GetAllCarRentsQueryHandler : IRequestHandler<GetAllCarRentsQuery, IEnumerable<CarRentDto>>
    {
        private readonly ICarRentRepository _carRentRepository;
        private readonly IConfigurationRepository _configurationRepository;

        public GetAllCarRentsQueryHandler(
            ICarRentRepository carRentRepository, 
            IConfigurationRepository configurationRepository)
        {
            _carRentRepository = carRentRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task<IEnumerable<CarRentDto>> Handle(GetAllCarRentsQuery request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationRepository.GetAsync();
            var cars = await _carRentRepository.GetAllAsync();
            return cars.Select(x => 
                new CarRentDto
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    CarStartMileage = x.CarStartMileage,
                    CarEndMileage = x.CarEndMileage,
                    CustomerBirthDate = x.CustomerBirthDate,
                    RentStartOn = x.RentStartOn,
                    RentEndOn = x.RentEndOn,
                    CarType = x.CarType.ToString(),
                    TotalPrice = x.CalculateTotalPrice(configuration.BaseDayRental, configuration.KilometerPrice)
                });
        }
    }
}
