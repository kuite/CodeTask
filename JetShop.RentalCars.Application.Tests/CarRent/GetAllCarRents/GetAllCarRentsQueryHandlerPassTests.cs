using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using JetShop.RentalCars.Application.CarRent.GetAllCarRents;
using JetShop.RentalCars.Domain.CarRents;
using Moq;
using Xunit;

namespace JetShop.RentalCars.Application.Tests.CarRent.GetAllCarRents
{
    public class GetAllCarRentsQueryHandlerPassTests
    {
        [Fact]
        public async Task Handle_WhenCalled_ReturnCorrectSize()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var carRents = fixture.CreateMany<Domain.CarRents.CarRent>();
            var mock = fixture.Freeze<Mock<ICarRentRepository>>();
            mock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(carRents);
            
            var query = fixture.Create<GetAllCarRentsQuery>();
            var handler = fixture.Create<GetAllCarRentsQueryHandler>();
            var token = fixture.Create<CancellationToken>();

            // Act
            var result = await handler.Handle(query, token);

            // Assert
            Assert.Equal(3, result.Count());

        }
    }
}
