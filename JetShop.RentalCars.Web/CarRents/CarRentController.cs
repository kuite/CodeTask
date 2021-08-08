using System.Threading.Tasks;
using JetShop.RentalCars.Application.CarRent.GetAllCarRents;
using JetShop.RentalCars.Application.CarRent.RentCar;
using JetShop.RentalCars.Web.CarRents.Requests;
using JetShop.RentalCars.Web.Cars.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JetShop.RentalCars.Web.CarRents
{
    [ApiController]
    [Route("[controller]")]
    public class CarRentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarRentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Post([FromBody]RentCarRequest request)
        {
            var carRent = await _mediator.Send(
                new CarRentCommand(request.CarId, request.RentStartOn, request.CustomerBirthDate, request.CarMileage));
            return Ok(carRent);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var carRents = await _mediator.Send(new GetAllCarRentsQuery());
            return Ok(carRents);
        }
    }
}
