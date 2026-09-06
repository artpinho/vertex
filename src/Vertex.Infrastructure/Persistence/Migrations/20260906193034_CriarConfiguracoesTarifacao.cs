using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarConfiguracoesTarifacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesTarifacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TipoMaquinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesTarifacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesTarifacao_TiposMaquina_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TiposMaquina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesTarifacao_TipoMaquinaId_Ativo_DataInicio",
                table: "ConfiguracoesTarifacao",
                columns: new[] { "TipoMaquinaId", "Ativo", "DataInicio" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesTarifacao");
        }
    }
}
