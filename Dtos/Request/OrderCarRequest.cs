using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBE.Dtos.Request
{
    public class OrderCarRequest
    {
        public int CarId { get; set; }
        public int Quantity { get; set; }
    }
}