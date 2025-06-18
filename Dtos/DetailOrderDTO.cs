using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;

namespace ProjectBE.Dtos
{
    public class DetailOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual OrderStatusEnum Status { get; set; }
        public virtual List<OrderCarsDTO>? Orders { get; set; }
    }
}