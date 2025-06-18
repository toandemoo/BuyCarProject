using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "carTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "refreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false),
                    Role = table.Column<string>(type: "NVARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    LicensePlate = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    PricePerDay = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CarTypeId = table.Column<int>(type: "INT", nullable: false),
                    BrandId = table.Column<int>(type: "INT", nullable: false),
                    BrandsId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_brands_BrandsId",
                        column: x => x.BrandsId,
                        principalTable: "brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cars_carTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "carTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "INT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "GETDATE()"),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_OrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishLists",
                columns: table => new
                {
                    Userid = table.Column<int>(type: "INT", nullable: false),
                    Carid = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishLists", x => new { x.Userid, x.Carid });
                    table.ForeignKey(
                        name: "FK_wishLists_cars_Carid",
                        column: x => x.Carid,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishLists_users_Userid",
                        column: x => x.Userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderCars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "INT", nullable: false),
                    OrderId = table.Column<int>(type: "INT", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderCars", x => new { x.OrderId, x.CarId });
                    table.ForeignKey(
                        name: "FK_orderCars_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderCars_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercedes-Benz" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nissan" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hyundai" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kia" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda" }
                });

            migrationBuilder.InsertData(
                table: "carTypes",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sedan" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SUV" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hatchback" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Convertible" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Coupe" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pickup" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minivan" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crossover" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wagon" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports Car" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 0, "Admin" },
                    { 1, "User" },
                    { 2, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "BrandId", "BrandsId", "CarTypeId", "ImageUrl", "LicensePlate", "Name", "PricePerDay", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "29A-12345", "Toyota Vios", 500000m, "Available" },
                    { 2, 2, null, 2, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "30B-67890", "Ford Everest", 800000m, "Available" },
                    { 3, 3, null, 1, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "31C-11111", "Honda Civic", 600000m, "Available" },
                    { 4, 4, null, 2, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "32D-22222", "BMW X5", 1500000m, "Available" },
                    { 5, 5, null, 1, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "33E-33333", "Mercedes C300", 1400000m, "Available" },
                    { 6, 6, null, 6, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "34F-44444", "Chevrolet Colorado", 700000m, "Available" },
                    { 7, 7, null, 6, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "35G-55555", "Nissan Navara", 750000m, "Available" },
                    { 8, 8, null, 2, "hhttps://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "36H-66666", "Hyundai SantaFe", 850000m, "Available" },
                    { 9, 9, null, 3, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "37K-77777", "Kia Morning", 400000m, "Available" },
                    { 10, 10, null, 2, "https://cafefcdn.com/203337114487263232/2024/11/21/scv-white-4-front-left-3601-1732167139690-1732167139799857346954.jpg", "38L-88888", "Mazda CX-5", 900000m, "Available" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_BrandId",
                table: "cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_BrandsId",
                table: "cars",
                column: "BrandsId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_CarTypeId",
                table: "cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_orderCars_CarId",
                table: "orderCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StatusId",
                table: "orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_wishLists_Carid",
                table: "wishLists",
                column: "Carid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderCars");

            migrationBuilder.DropTable(
                name: "refreshTokens");

            migrationBuilder.DropTable(
                name: "wishLists");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "carTypes");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
