using System;
using System.Collections.Generic;
using System.Text;
using JetShop.RentalCars.Domain.SeedWork;

namespace JetShop.RentalCars.Domain.Cars
{
    public class Car : Entity
    {
        public CarType Type { get; set; }

        public int Mileage { get; set; }

        public CarStatus Status { get; set; }

        public void UpdateMileage(int newMileage)
        {
            if (Mileage > newMileage)
            {
                throw new InvalidOperationException("New value can not be lower than actual");
            }
            Mileage = newMileage;
        }
    }
}
