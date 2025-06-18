using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;

namespace ProjectBE.Dtos
{
    public class OrderCarsDTO
    {
        public int CarId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public CarDTO? Car { get; set; }
    }
}