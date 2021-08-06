using System;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.ReturnCar
{
    public class CarReturnCommand : IRequest<CarRentDto>
    {
        public DateTime ReturnDateTime { get; set; }

        public Guid CarRentId { get; set; }

        public int CarMileage { get; set; }

        public CarReturnCommand(Guid carRentId, DateTime returnDateTime, int carMileage)
        {
            CarRentId = carRentId;
            CarMileage = carMileage;
            ReturnDateTime = returnDateTime;
        }
    }
}
