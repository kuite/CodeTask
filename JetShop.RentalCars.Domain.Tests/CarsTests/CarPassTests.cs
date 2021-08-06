using AutoFixture;
using JetShop.RentalCars.Domain.Cars;
using Xunit;

namespace JetShop.RentalCars.Domain.Tests.CarsTests
{
    public class CarPassTests
    {
        [Fact]
        public void UpdateMileage_WhenCorrect_UpdatesCorrect()
        {
            // Arrange
            var fixture = new Fixture();
            var oldMileage = 2;
            var newMileage = oldMileage + 5;
            var car = fixture.Build<Car>()
                .With(x => x.Mileage, oldMileage)
                .Create();

            // Act
            car.UpdateMileage(newMileage);

            // Assert
            Assert.Equal(newMileage, car.Mileage);

        }
    }
}
