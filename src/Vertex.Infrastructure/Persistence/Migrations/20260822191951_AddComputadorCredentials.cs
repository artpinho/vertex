using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddComputadorCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputadorCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComputadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecretHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRevogacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputadorCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputadorCredentials_Computadores_ComputadorId",
                        column: x => x.ComputadorId,
                        principalTable: "Computadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computadores_HostName",
                table: "Computadores",
                column: "HostName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computadores_MacAddress",
                table: "Computadores",
                column: "MacAddress",
                unique: true,
                filter: "[MacAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ComputadorCredentials_ClientId",
                table: "ComputadorCredentials",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComputadorCredentials_ComputadorId",
                table: "ComputadorCredentials",
                column: "ComputadorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputadorCredentials");

            migrationBuilder.DropIndex(
                name: "IX_Computadores_HostName",
                table: "Computadores");

            migrationBuilder.DropIndex(
                name: "IX_Computadores_MacAddress",
                table: "Computadores");
        }
    }
}
