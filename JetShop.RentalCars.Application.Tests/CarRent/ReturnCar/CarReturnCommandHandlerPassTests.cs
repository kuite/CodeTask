using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.CarRent.ReturnCar;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.CarRent.ReturnCar
{
    public class CarReturnCommandHandlerPassTests
    {
        [Fact]
        public async Task Handle_WhenCalled_UpdateCarStatusMileage()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var startMileage = 1;
            var endMileage = 6;
            var car = fixture.Build<Car>()
                .With(x => x.Mileage, startMileage)
                .With(x => x.Status, CarStatus.NotAvailable)
                .Create();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);
            var carRents = fixture.CreateMany<Domain.CarRents.CarRent>();
            var mockCarRentRepository = fixture.Freeze<Mock<ICarRentRepository>>();
            mockCarRentRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(carRents);

            var query = fixture.Build<CarReturnCommand>()
                .With(x => x.CarMileage, endMileage)
                .Create();
            var handler = fixture.Create<CarReturnCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            await handler.Handle(query, token);

            // Assert
            mockCarRepository.Verify(x => x.UpdateAsync(It.IsAny<Car>()));
            Assert.Equal(CarStatus.Available, car.Status);
            Assert.Equal(endMileage, car.Mileage);
        }

        [Fact]
        public async Task Handle_WhenCalled_UpdateCarRent()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var startMileage = 1;
            var endMileage = 6;
            var rentEndDate = DateTime.Now;
            var car = fixture.Build<Car>()
                .With(x => x.Mileage, startMileage)
                .Create();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(car);
            var carRent = fixture.Create<Domain.CarRents.CarRent>();
            var mockCarRentRepository = fixture.Freeze<Mock<ICarRentRepository>>();
            mockCarRentRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(carRent);

            var query = fixture.Build<CarReturnCommand>()
                .With(x => x.CarMileage, endMileage)
                .With(x => x.ReturnDateTime, rentEndDate)
                .Create();
            var handler = fixture.Create<CarReturnCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            await handler.Handle(query, token);

            // Assert
            mockCarRentRepository.Verify(x => x.UpdateAsync(It.IsAny<Domain.CarRents.CarRent>()));
            Assert.Equal(rentEndDate, carRent.RentEndOn);
            Assert.Equal(endMileage, carRent.CarEndMileage);
        }
    }
}
