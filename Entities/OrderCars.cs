using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class OrderCars
    {
        public int CarId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public virtual Cars? Car { get; set; }
    }
}