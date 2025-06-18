using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_brands_BrandsId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_OrderStatus_StatusId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_users_RoleId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_orders_StatusId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_cars_BrandsId",
                table: "cars");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "BrandsId",
                table: "cars");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "orders",
                type: "NVARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "cars",
                type: "NVARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "users",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "orders",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "cars",
                type: "NVARCHAR(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)");

            migrationBuilder.AddColumn<int>(
                name: "BrandsId",
                table: "cars",
                type: "INT",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 6,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 7,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 9,
                column: "BrandsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 10,
                column: "BrandsId",
                value: null);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 0, "Admin" },
                    { 1, "User" },
                    { 2, "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StatusId",
                table: "orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_BrandsId",
                table: "cars",
                column: "BrandsId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_brands_BrandsId",
                table: "cars",
                column: "BrandsId",
                principalTable: "brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_OrderStatus_StatusId",
                table: "orders",
                column: "StatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
