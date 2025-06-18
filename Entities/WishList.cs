using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class WishList
    {
        public int Userid { get; set; }
        public int Carid { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        public virtual Cars? Cars { get; set; }
        public virtual Users? Users { get; set; }
    }
}