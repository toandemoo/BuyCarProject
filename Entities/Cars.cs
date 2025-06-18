using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ProjectBE.Entities;

namespace Project.Entities
{
    public enum CarStatusEnum
    {
        Available = 0,
        Rented = 1,
        Maintenance = 2,
        Reserved = 3,
        Inactive = 4
    }

    public class Cars
    {
        [Key, Column("Id", TypeName = "INT")]
        public int Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }

        [Column("LicensePlate", TypeName = "NVARCHAR(100)")]
        public string? LicensePlate { get; set; }

        [Column("PricePerDay", TypeName = "DECIMAL(18,2)")]
        public double PricePerDay { get; set; }

        [Column("ImageUrl", TypeName = "NVARCHAR(MAX)")]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        [Column("CarTypeId", TypeName = "INT")]
        public int CarTypeId { get; set; }

        [Column("BrandId", TypeName = "INT")]
        public int BrandId { get; set; }

        [Column("Status", TypeName = "NVARCHAR(100)")]
        public CarStatusEnum Status { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brands? Brands { get; set; }
        [ForeignKey("CarTypeId")]
        public virtual CarTypes? CarTypes { get; set; }
        [ForeignKey("CarId")]
        public virtual List<OrderCars>? OrderCars { get; set; }
        [ForeignKey("WishListId")]
        public virtual List<WishList>? WishList { get; set; }
    }
}