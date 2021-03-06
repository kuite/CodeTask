using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.CarRent.RentCar;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.CarRent.RentCar
{
    public class CarRentCommandHandlerPassTests
    {
        [Fact]
        public async Task Handle_WhenCalled_CreateNewCarRent()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var carMileage = 555;
            var car = fixture.Build<Car>()
                .With(x => x.Status, CarStatus.Available)
                .With(x => x.Mileage, carMileage)
                .Create();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);
            var mockCarRentRepository = fixture.Freeze<Mock<ICarRentRepository>>();

            var query = fixture.Build<CarRentCommand>()
                .With(x => x.CarMileage, carMileage + 111)
                .Create();
            var handler = fixture.Create<CarRentCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            await handler.Handle(query, token);

            // Assert
            mockCarRentRepository.Verify(x => x.Create(It.IsAny<Domain.CarRents.CarRent>()));
        }

        [Fact]
        public async Task Handle_WhenCalled_UpdateCarStatusMileage()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var carMileage = 555;
            var endMileage = carMileage + 111;
            var car = fixture.Build<Car>()
                .With(x => x.Status, CarStatus.Available)
                .With(x => x.Mileage, carMileage)
                .Create();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);

            var query = fixture.Build<CarRentCommand>()
                .With(x => x.CarMileage, endMileage)
                .Create();
            var handler = fixture.Create<CarRentCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            await handler.Handle(query, token);

            // Assert
            Assert.Equal(CarStatus.NotAvailable, car.Status);
            Assert.Equal(endMileage, car.Mileage);
        }
    }
}
