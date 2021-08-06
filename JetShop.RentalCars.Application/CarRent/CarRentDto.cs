using System;

namespace JetShop.RentalCars.Application.CarRent
{
    public class CarRentDto
    {
        public Guid Id { get; set; }

        public DateTime CustomerBirthDate { get; set; }

        public Guid CarId { get; set; }

        public int CarStartMileage { get; set; }

        public int CarEndMileage { get; set; }

        public DateTime RentEndOn { get; set; }

        public DateTime RentStartOn { get; set; }

        public string CarType { get; set; }

        public double TotalPrice { get; set; }
    }
}
