using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProjectBE.Entities;

namespace Project.Entities
{
    public enum OrderStatusEnum
    {
        Pending = 0,
        Confirmed = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5,
        Returned = 6,
        Refunded = 7
    }
    public class Orders
    {
        [Key, Column("Id", TypeName = "INT")]
        public int Id { get; set; }

        [Column("UserId", TypeName = "INT")]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Column("CreatedAt", TypeName = "DATE")]
        public DateTime CreatedAt { get; set; }

        [Column("Status", TypeName = "NVARCHAR(100)")]
        public OrderStatusEnum Status { get; set; }

        //navigation
        public virtual List<OrderCars>? OrderCars { get; set; }
        public virtual Users? Users { get; set; }
    }
}