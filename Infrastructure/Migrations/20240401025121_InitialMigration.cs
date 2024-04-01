using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeUser = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exchange = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Document", "LastName", "Name", "Password", "PhoneNumber", "TypeUser", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, "1088027397", "Franco Herrera", "Jorge Enrique", "12345678a", "3148434889", 1, null },
                    { 2, null, "1094952038", "Portela Guerra", "Juan Luis", "12345678a", "3127248659", 2, null },
                    { 3, null, "1088352391", "Perez Ortega", "Natalia", "12345678a", "3022901238", 2, null }
                });

            migrationBuilder.InsertData(
                table: "Branch",
                columns: new[] { "Id", "Address", "CreateDate", "Email", "Exchange", "Name", "PhoneNumber", "Schedule", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Calle 44 # 12 - 70 Barrio Las Camelias", new DateTime(2024, 3, 31, 16, 18, 42, 698, DateTimeKind.Local).AddTicks(8983), "gerencia@alpina.com", 2, "Alpina", "3154096906", "8:00am - 6:00pm", null, 2 },
                    { 2, "Calle 44 # 12 - 70 Barrio Las Camelias", new DateTime(2024, 3, 29, 22, 44, 34, 258, DateTimeKind.Unspecified).AddTicks(3444), "gerencia@colanta.com", 2, "Colanta", "3154096906", "8:00am - 6:00pm", null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_UserId",
                table: "Branch",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
