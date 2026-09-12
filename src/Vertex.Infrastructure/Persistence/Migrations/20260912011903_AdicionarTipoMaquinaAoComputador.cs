using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTipoMaquinaAoComputador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipoMaquinaId",
                table: "Computadores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computadores_TipoMaquinaId",
                table: "Computadores",
                column: "TipoMaquinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computadores_TiposMaquina_TipoMaquinaId",
                table: "Computadores",
                column: "TipoMaquinaId",
                principalTable: "TiposMaquina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computadores_TiposMaquina_TipoMaquinaId",
                table: "Computadores");

            migrationBuilder.DropIndex(
                name: "IX_Computadores_TipoMaquinaId",
                table: "Computadores");

            migrationBuilder.DropColumn(
                name: "TipoMaquinaId",
                table: "Computadores");
        }
    }
}
