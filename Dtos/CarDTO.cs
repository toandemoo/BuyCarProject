using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using ProjectBE.Entities;

namespace Project.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LicensePlate { get; set; }
        public double PricePerDay { get; set; }
        public string? ImageUrl { get; set; }
        public CarStatusEnum Status { get; set; }
        public string? BrandName { get; set; }
        public string? CarType { get; set; }
        public int BrandId { get; set; }
        public int CarTypeId { get; set; }

        public CarDTO(Cars cars)
        {
            if (cars is null)
            {
                throw new ArgumentNullException(nameof(cars));
            }

            Id = cars.Id;
            Name = cars.Name;
            LicensePlate = cars.LicensePlate;
            PricePerDay = cars.PricePerDay;
            ImageUrl = cars.ImageUrl;
            Status = cars.Status;
            BrandName = cars.Brands?.Name;
            CarType = cars.CarTypes?.Name;
            BrandId = cars.BrandId;
            CarTypeId = cars.CarTypeId;
        }
    }
}