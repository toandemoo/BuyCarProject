using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectBE.Dtos.Request
{
    public class UpdateWishlistRequest
    {
        [JsonPropertyName("quantity")]
        public int quantity { get; set; }
    }
}