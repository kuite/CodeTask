using System;
using System.ComponentModel.DataAnnotations;

namespace JetShop.RentalCars.Web.CarRents.Requests
{
    public class RentCarRequest
    {
        [Required]
        public Guid CarId { get; set; }

        [Required]
        public int CarMileage { get; set; }

        [Required]
        public DateTime RentStartOn { get; set; }

        [Required]
        public DateTime CustomerBirthDate { get; set; }
    }
}
