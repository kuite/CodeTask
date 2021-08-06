using System.Collections.Generic;
using MediatR;

namespace JetShop.RentalCars.Application.CarRent.GetAllCarRents
{
    public class GetAllCarRentsQuery : IRequest<IEnumerable<CarRentDto>>
    {
        public GetAllCarRentsQuery()
        {

        }
    }
}
