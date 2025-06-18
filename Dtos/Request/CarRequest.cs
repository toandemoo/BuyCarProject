using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using ProjectBE.Entities;

namespace Project.DTOs
{
    public class CarRequest
    {
        public String? Name { get; set; }
        public String? LicensePlate { get; set; }
        public int CarTypeId { get; set; }
        public int BrandId { get; set; }
        public Double PricePerDay { get; set; }
        public String? ImageUrl { get; set; }
        public CarStatusEnum Status { get; set; }
    }
}