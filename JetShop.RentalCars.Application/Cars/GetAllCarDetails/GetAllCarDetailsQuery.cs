using System;
using System.Collections.Generic;
using MediatR;

namespace JetShop.RentalCars.Application.Cars.GetAllCarDetails
{
    public class GetAllCarDetailsQuery : IRequest<IEnumerable<CarDetailsDto>>
    {
        public GetAllCarDetailsQuery()
        {

        }
    }
}
