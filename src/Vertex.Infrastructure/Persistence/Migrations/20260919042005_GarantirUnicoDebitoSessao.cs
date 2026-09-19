using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GarantirUnicoDebitoSessao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesCarteira_SessaoId",
                table: "MovimentacoesCarteira",
                column: "SessaoId",
                unique: true,
                filter: "[SessaoId] IS NOT NULL AND [Tipo] = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesCarteira_SessaoId",
                table: "MovimentacoesCarteira");
        }
    }
}
