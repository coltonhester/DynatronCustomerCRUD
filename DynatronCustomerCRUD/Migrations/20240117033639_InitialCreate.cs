using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DynatronCustomerCRUD.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "LastUpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5206), "bobdillon@example.com", "Bob", "Sclansky", new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5252) },
                    { 2, new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5253), "elroysmith@example.com", "Elroy", "Smith", new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5254) },
                    { 3, new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5255), "taixiaoma@example.com", "Tai", "Xiaoma", new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5256) },
                    { 4, new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5257), "kevinbacon@example.com", "Kevin", "Bacon", new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5258) },
                    { 5, new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5259), "alenazarcovia@example.com", "Alena", "Zarcovia", new DateTime(2024, 1, 16, 19, 36, 39, 233, DateTimeKind.Local).AddTicks(5260) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
