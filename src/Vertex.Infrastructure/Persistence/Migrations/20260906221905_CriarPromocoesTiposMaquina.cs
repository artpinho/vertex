using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarPromocoesTiposMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromocoesTiposMaquina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMaquinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocoesTiposMaquina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromocoesTiposMaquina_Promocoes_PromocaoId",
                        column: x => x.PromocaoId,
                        principalTable: "Promocoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocoesTiposMaquina_TiposMaquina_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TiposMaquina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromocoesTiposMaquina_PromocaoId_TipoMaquinaId",
                table: "PromocoesTiposMaquina",
                columns: new[] { "PromocaoId", "TipoMaquinaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromocoesTiposMaquina_TipoMaquinaId",
                table: "PromocoesTiposMaquina",
                column: "TipoMaquinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromocoesTiposMaquina");
        }
    }
}
