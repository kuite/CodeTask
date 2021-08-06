using System;

namespace JetShop.RentalCars.Application.Cars
{
    public class CarDetailsDto
    {
        public Guid Id { get; set; }

        public int Mileage { get; set; }

        public string CarType { get; set; }

        public string Status { get; set; }
    }
}
