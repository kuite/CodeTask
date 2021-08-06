using System.Threading.Tasks;
using JetShop.RentalCars.Application.CarRent.ReturnCar;
using JetShop.RentalCars.Web.Cars.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JetShop.RentalCars.Web.Cars
{
    [ApiController]
    [Route("[controller]")]
    public class CarReturnController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarReturnController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Post([FromBody]ReturnCarRequest request)
        {
            var carReturn = await _mediator.Send(
                new CarReturnCommand(request.CarRentId, request.ReturnDateTime, request.CurrentCarMileage));
            return Ok(carReturn);
        }
    }
}
