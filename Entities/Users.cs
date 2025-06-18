using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProjectBE.Entities;

namespace Project.Entities
{
    public enum RoleEnum

    {
        Admin,
        User,
        Manager,
        Seller,
        Customer,
        Shipper,
        Accountant
    }

    public class Users
    {
        [Key, Column("Id", TypeName = "INT")]
        public int Id { get; set; }

        [Column("FullName", TypeName = "NVARCHAR(100)")]
        public string? FullName { get; set; }

        [Column("Email", TypeName = "NVARCHAR(100)")]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("Password", TypeName = "NVARCHAR(100)")]
        public string? Password { get; set; }

        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Column("profileImage")]
        public string? profileImage { get; set; }

        [Column("Role", TypeName = "NVARCHAR(100)")]
        public RoleEnum? Role { get; set; }

        [Column("CreatedAt", TypeName = "DATETIME")]
        public DateTime CreatedAt { get; set; }

        public bool EmailConfirmed { get; set; } = false;
    }
}