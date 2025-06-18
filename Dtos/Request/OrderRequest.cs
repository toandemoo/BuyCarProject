using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using ProjectBE.Dtos.Request;
using ProjectBE.Entities;

namespace Project.Request
{
    public class OrderRequest
    {
        public OrderStatusEnum Status { get; set; }
        public List<OrderCarRequest> orderCarRequest { get; set; }
    }
}