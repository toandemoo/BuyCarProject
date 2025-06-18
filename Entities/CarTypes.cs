using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class CarTypes
    {
        [Key, Column("Id", TypeName = "INT")]
        public int Id { get; set; }
        [Column("Name", TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<Cars>? Cars { get; set; }
    }
}