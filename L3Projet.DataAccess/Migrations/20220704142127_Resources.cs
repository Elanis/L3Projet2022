using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L3Projet.DataAccess.Migrations
{
    public partial class Resources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PlanetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => new { x.Type, x.PlanetId });
                    table.ForeignKey(
                        name: "FK_Resource_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_PlanetId",
                table: "Resource",
                column: "PlanetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
