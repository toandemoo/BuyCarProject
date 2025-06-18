using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Project.Entities;

namespace ProjectBE.Dtos.Request
{
    public class OrderUpdateRequest
    {
        [JsonPropertyName("OrderStatus")]
        public OrderStatusEnum orderStatusEnum { get; set; }
    }
}