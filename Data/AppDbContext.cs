using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Entities;
using ProjectBE.Entities;
using StackExchange.Redis;

namespace Project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Brands> brands { get; set; }
        public DbSet<Cars> cars { get; set; }
        public DbSet<CarTypes> carTypes { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<OrderCars> orderCars { get; set; }
        public DbSet<WishList> wishLists { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                          // .UseLazyLoadingProxies()
                          //   .UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString"))
                          .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName() ?? throw new Exception("Table name not found.");
                if (tableName.StartsWith("AspNet"))
                {
                    entity.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<Cars>()
                        .HasOne(a => a.Brands)
                        .WithMany(a => a.Cars)
                        .HasForeignKey(c => c.BrandId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cars>()
                        .HasOne(a => a.CarTypes)
                        .WithMany(t => t.Cars)
                        .HasForeignKey(a => a.CarTypeId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cars>()
                        .Property(c => c.CreatedAt)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Orders>()
                        .HasOne(a => a.Users)
                        .WithMany()
                        .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Orders>()
                        .HasMany(a => a.OrderCars)
                        .WithOne()
                        .HasForeignKey(a => a.OrderId);

            modelBuilder.Entity<Orders>()
                        .Property(o => o.CreatedAt)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Users>()
                        .Property(u => u.CreatedAt)
                        .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<OrderCars>()
                        .HasKey(rc => new { rc.OrderId, rc.CarId });

            modelBuilder.Entity<OrderCars>()
                        .HasOne(rc => rc.Car)
                        .WithMany(c => c.OrderCars)
                        .HasForeignKey(rc => rc.CarId);

            modelBuilder.Entity<WishList>()
                        .HasKey(w => new { w.Userid, w.Carid });


            modelBuilder.Entity<WishList>()
                        .HasOne(w => w.Users)
                        .WithMany()
                        .HasForeignKey(w => w.Userid);

            modelBuilder.Entity<WishList>()
                        .HasOne(w => w.Cars)
                        .WithMany(c => c.WishList)
                        .HasForeignKey(w => w.Carid);


            modelBuilder.Entity<CarTypes>().HasData(
                new CarTypes { Id = 1, Name = "Sedan" },
                new CarTypes { Id = 2, Name = "SUV" },
                new CarTypes { Id = 3, Name = "Hatchback" },
                new CarTypes { Id = 4, Name = "Convertible" },
                new CarTypes { Id = 5, Name = "Coupe" },
                new CarTypes { Id = 6, Name = "Pickup" },
                new CarTypes { Id = 7, Name = "Minivan" },
                new CarTypes { Id = 8, Name = "Crossover" },
                new CarTypes { Id = 9, Name = "Wagon" },
                new CarTypes { Id = 10, Name = "Sports Car" }
            );

            modelBuilder.Entity<Brands>().HasData(
                new Brands { Id = 1, Name = "Toyota" },
                new Brands { Id = 2, Name = "Ford" },
                new Brands { Id = 3, Name = "Honda" },
                new Brands { Id = 4, Name = "BMW" },
                new Brands { Id = 5, Name = "Mercedes-Benz" },
                new Brands { Id = 6, Name = "Chevrolet" },
                new Brands { Id = 7, Name = "Nissan" },
                new Brands { Id = 8, Name = "Hyundai" },
                new Brands { Id = 9, Name = "Kia" },
                new Brands { Id = 10, Name = "Mazda" }
            );

            modelBuilder.Entity<Cars>().HasData(
                new Cars
                {
                    Id = 1,
                    Name = "Toyota Vios",
                    LicensePlate = "29A-12345",
                    CarTypeId = 1,
                    BrandId = 1,
                    PricePerDay = 500000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 2,
                    Name = "Ford Everest",
                    LicensePlate = "30B-67890",
                    CarTypeId = 2,
                    BrandId = 2,
                    PricePerDay = 800000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 3,
                    Name = "Honda Civic",
                    LicensePlate = "31C-11111",
                    CarTypeId = 1,
                    BrandId = 3,
                    PricePerDay = 600000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 4,
                    Name = "BMW X5",
                    LicensePlate = "32D-22222",
                    CarTypeId = 2,
                    BrandId = 4,
                    PricePerDay = 1500000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 5,
                    Name = "Mercedes C300",
                    LicensePlate = "33E-33333",
                    CarTypeId = 1,
                    BrandId = 5,
                    PricePerDay = 1400000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 6,
                    Name = "Chevrolet Colorado",
                    LicensePlate = "34F-44444",
                    CarTypeId = 6,
                    BrandId = 6,
                    PricePerDay = 700000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 7,
                    Name = "Nissan Navara",
                    LicensePlate = "35G-55555",
                    CarTypeId = 6,
                    BrandId = 7,
                    PricePerDay = 750000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 8,
                    Name = "Hyundai SantaFe",
                    LicensePlate = "36H-66666",
                    CarTypeId = 2,
                    BrandId = 8,
                    PricePerDay = 850000,
                    ImageUrl = "hhttps://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 9,
                    Name = "Kia Morning",
                    LicensePlate = "37K-77777",
                    CarTypeId = 3,
                    BrandId = 9,
                    PricePerDay = 400000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                },
                new Cars
                {
                    Id = 10,
                    Name = "Mazda CX-5",
                    LicensePlate = "38L-88888",
                    CarTypeId = 2,
                    BrandId = 10,
                    PricePerDay = 900000,
                    ImageUrl = "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg",
                    Status = CarStatusEnum.Available
                }
            );

        }
    }
}