using System.Threading.Tasks;
using JetShop.RentalCars.Application.Cars.GetAllCarDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JetShop.RentalCars.Web.Cars
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var cars = await _mediator.Send(new GetAllCarDetailsQuery());
            return Ok(cars);
        }
    }
}
