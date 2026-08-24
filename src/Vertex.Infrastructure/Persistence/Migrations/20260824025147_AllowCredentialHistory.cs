using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllowCredentialHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComputadorCredentials_ComputadorId",
                table: "ComputadorCredentials");

            migrationBuilder.CreateIndex(
                name: "IX_ComputadorCredentials_ComputadorId",
                table: "ComputadorCredentials",
                column: "ComputadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComputadorCredentials_ComputadorId",
                table: "ComputadorCredentials");

            migrationBuilder.CreateIndex(
                name: "IX_ComputadorCredentials_ComputadorId",
                table: "ComputadorCredentials",
                column: "ComputadorId",
                unique: true);
        }
    }
}
