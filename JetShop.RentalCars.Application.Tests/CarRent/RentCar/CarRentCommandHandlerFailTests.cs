using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.CarRent.RentCar;
using JetShop.RentalCars.Domain.Cars;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.CarRent.RentCar
{
    public class CarRentCommandHandlerFailTests
    {
        [Fact]
        public async Task Handle_WhenCarNotFound_ThrowException()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Car) null);

            var query = fixture.Create<CarRentCommand>();
            var handler = fixture.Create<CarRentCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(query, token));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task Handle_WhenCarNotAvailable_ThrowException()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var car = fixture.Build<Car>()
                .With(x => x.Status, CarStatus.NotAvailable)
                .Create();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);

            var query = fixture.Create<CarRentCommand>();
            var handler = fixture.Create<CarRentCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(query, token));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
