using System;
using System.Threading;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using JetShop.RentalCars.Domain.Configuration;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.ReturnCar
{
    public class CarReturnCommandHandler : IRequestHandler<CarReturnCommand, CarRentDto>
    {
        private readonly ICarRentRepository _carRentRepository;
        private readonly ICarRepository _carRepository;
        private readonly IConfigurationRepository _configurationRepository;

        public CarReturnCommandHandler(
            ICarRentRepository carRentRepository, 
            ICarRepository carRepository, 
            IConfigurationRepository configurationRepository)
        {
            _carRentRepository = carRentRepository;
            _carRepository = carRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task<CarRentDto> Handle(CarReturnCommand request, CancellationToken cancellationToken)
        {
            var carRent = await _carRentRepository.GetByIdAsync(request.CarRentId);
            if (carRent == null)
            {
                throw new ArgumentException($"Car rent of id {request.CarRentId} not found");
            }

            var car = await _carRepository.GetByIdAsync(carRent.CarId);
            if (car == null)
            {
                throw new InvalidOperationException($"Car of id {carRent.CarId} not found");
            }

            car.UpdateMileage(request.CarMileage);
            car.Status = CarStatus.Available;
            await _carRepository.UpdateAsync(car);

            carRent.CarEndMileage = request.CarMileage;
            carRent.RentEndOn = request.ReturnDateTime;
            var updatedRent = await _carRentRepository.UpdateAsync(carRent);

            var configuration = await _configurationRepository.GetAsync();

            return new CarRentDto
            {
                Id = updatedRent.Id,
                CarId = updatedRent.CarId,
                CarStartMileage = updatedRent.CarStartMileage,
                CarEndMileage = updatedRent.CarEndMileage,
                CustomerBirthDate = updatedRent.CustomerBirthDate,
                RentStartOn = updatedRent.RentStartOn,
                RentEndOn = updatedRent.RentEndOn,
                CarType = updatedRent.CarType.ToString(),
                TotalPrice = updatedRent.CalculateTotalPrice(configuration.BaseDayRental, configuration.KilometerPrice)
            };
        }
    }
}
