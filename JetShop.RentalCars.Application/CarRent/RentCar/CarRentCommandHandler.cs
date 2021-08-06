using System;
using System.Threading;
using System.Threading.Tasks;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.RentCar
{
    public class CarRentCommandHandler : IRequestHandler<CarRentCommand, Guid>
    {
        private readonly ICarRentRepository _carRentRepository;
        private readonly ICarRepository _carRepository;

        public CarRentCommandHandler(
            ICarRentRepository carRentRepository, 
            ICarRepository carRepository)
        {
            _carRentRepository = carRentRepository;
            _carRepository = carRepository;
        }

        public async Task<Guid> Handle(CarRentCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.CarId);
            if (car == null)
            {
                throw new InvalidOperationException($"Car of id {request.CarId} not found");
            }

            if (car.Status != CarStatus.Available)
            {
                throw new InvalidOperationException($"Can not rent car of status {car.Status}");
            }

            car.UpdateMileage(request.CarMileage);
            car.Status = CarStatus.NotAvailable;
            await _carRepository.UpdateAsync(car);

            var newCarRent = new Domain.CarRents.CarRent
            {
                Id = Guid.NewGuid(),
                CustomerBirthDate = request.CustomerBirthDate,
                RentStartOn = request.RentStartOn,
                CarId = request.CarId,
                CarStartMileage = request.CarMileage,
                CarType = car.Type
            };
            return await _carRentRepository.Create(newCarRent);
        }
    }
}
