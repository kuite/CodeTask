using System;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.RentCar
{
    public class CarRentCommand : IRequest<Guid>
    {
        public DateTime CustomerBirthDate { get; set; }

        public DateTime RentStartOn { get; set; }

        public Guid CarId { get; set; }

        public int CarMileage { get; set; }

        public CarRentCommand(Guid carId, DateTime requestRentStart, DateTime customerBirthDate, int carMileage)
        {
            CarId = carId;
            CarMileage = carMileage;
            RentStartOn = requestRentStart;
            CustomerBirthDate = customerBirthDate;
        }
    }
}
