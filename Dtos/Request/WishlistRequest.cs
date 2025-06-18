using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBE.Dtos.Request
{
    public class WishlistRequest
    {
        public int UserId { get; set; }
        public List<int> CarIds { get; set; }
    }
}