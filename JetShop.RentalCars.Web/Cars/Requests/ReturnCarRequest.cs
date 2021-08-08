using System;

namespace JetShop.RentalCars.Web.Cars.Requests
{
    public class ReturnCarRequest
    {
        public Guid CarRentId { get; set; }

        public int CurrentCarMileage { get; set; }

        public DateTime ReturnDateTime { get; set; }
    }
}
