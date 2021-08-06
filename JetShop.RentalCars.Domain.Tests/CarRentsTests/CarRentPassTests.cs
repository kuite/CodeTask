using System;
using AutoFixture;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using Xunit;

namespace JetShop.RentalCars.Domain.Tests.CarRentsTests
{
    public class CarRentPassTests
    {
        [Theory]
        [InlineData(CarType.Compact, 2, 1.5, 1.4, 50, 3)]
        [InlineData(CarType.Minivan, 2, 1.5, 1.4, 50, 110.1)]
        [InlineData(CarType.Premium, 2, 1.5, 1.4, 50, 73.6)]
        [InlineData(CarType.Compact, 8, 1.5, 1.4, 40, 12)]
        [InlineData(CarType.Minivan, 8, 1.5, 1.4, 40, 104.4)]
        [InlineData(CarType.Premium, 8, 1.5, 1.4, 40, 70.4)]
        public void CalculateTotalPrice_WhenValidInput_ReturnsCorrectPrice(
            CarType carType, 
            int durationDays,
            double baseDayRental,
            double kilometerPrice, 
            int mileage, 
            double expectedPrice)
        {
            // Arrange
            var fixture = new Fixture();
            var rentStartOn = DateTime.Now;
            var rentEndOn = rentStartOn.AddDays(durationDays);
            var startMileage = 1;
            var endMileage = startMileage + mileage;
            var carRent = fixture.Build<CarRent>()
                .With(x => x.CarStartMileage, startMileage)
                .With(x => x.CarEndMileage, endMileage)
                .With(x => x.RentStartOn, rentStartOn)
                .With(x => x.RentEndOn, rentEndOn)
                .With(x => x.CarType, carType)
                .Create();

            // Act
            var price = carRent.CalculateTotalPrice(baseDayRental, kilometerPrice);

            // Assert
            Assert.Equal(expectedPrice, price);

        }
    }
}
