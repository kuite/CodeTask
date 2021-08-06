using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.CarRent.RentCar;
using JetShop.RentalCars.Application.Cars.GetAllCarDetails;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.CarRent.RentCar
{
    public class CarRentCommandHandlerPassTests
    {
        [Fact]
        public async Task CarRentCommandHandler_WhenCalled_CreateNewCarRent()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var cars = fixture.CreateMany<Car>();
            var mockCarRepository = fixture.Freeze<Mock<ICarRepository>>();
            mockCarRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(cars);
            var carRents = fixture.CreateMany<Domain.CarRents.CarRent>();
            var mockCarRentRepository = fixture.Freeze<Mock<ICarRentRepository>>();
            mockCarRentRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(carRents);

            var query = fixture.Create<CarRentCommand>();
            var handler = fixture.Create<CarRentCommandHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            var result = await handler.Handle(query, token);

            // Assert
            mockCarRentRepository.Verify(x => x.Create(It.IsAny<Domain.CarRents.CarRent>()));

        }
    }
}
