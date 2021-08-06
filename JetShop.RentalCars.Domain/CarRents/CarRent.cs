using System;
using JetShop.RentalCars.Domain.Cars;
using JetShop.RentalCars.Domain.SeedWork;

namespace JetShop.RentalCars.Domain.CarRents
{
    public class CarRent : Entity
    {
        public Guid CarId { get; set; }

        public virtual Car Car { get; set; }

        public DateTime CustomerBirthDate { get; set; }

        public DateTime RentStartOn { get; set; }

        public DateTime RentEndOn { get; set; }

        public int CarStartMileage { get; set; }

        public int CarEndMileage { get; set; }

        public CarType CarType { get; set; }

        public double CalculateTotalPrice(double baseDayRental, double kilometerPrice)
        {
            var price = 0d;
            if (RentStartOn > RentEndOn)
            {
                return price;
            }

            var rentDaysLong = Math.Ceiling((RentEndOn - RentStartOn).TotalDays);
            var numberOfKilometers = CarEndMileage - CarStartMileage;
            switch (CarType)
            {
                case CarType.Compact:
                    price = baseDayRental* rentDaysLong;
                    break;
                case CarType.Premium:
                    price = baseDayRental * rentDaysLong * 1.2 + (kilometerPrice * numberOfKilometers);
                    break;
                case CarType.Minivan:
                    price = baseDayRental * rentDaysLong * 1.7 + (kilometerPrice * numberOfKilometers * 1.5);
                    break;
                default:
                    throw new InvalidOperationException($"Car type can not be null of CarRent id {Id}");
            }

            return Math.Round(price, 2);
        }

    }
}
