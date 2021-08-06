using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.Cars.GetAllCarDetails;
using JetShop.RentalCars.Domain.Cars;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.Cars.GetAllCarDetails
{
    public class GetAllCarDetailsQueryHandlerPassTests
    {
        [Fact]
        public async Task Handle_WhenCalled_ReturnCorrectSize()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var cars = fixture.CreateMany<Car>();
            var mock = fixture.Freeze<Mock<ICarRepository>>();
            mock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(cars);
            
            var query = fixture.Create<GetAllCarDetailsQuery>();
            var handler = fixture.Create<GetAllCarDetailsQueryHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            var result = await handler.Handle(query, token);

            // Assert
            Assert.Equal(3, result.Count());

        }
    }
}
