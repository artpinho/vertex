using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarFaixasHorarioTarifacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaixasHorarioTarifacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfiguracaoTarifacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "time", nullable: false),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixasHorarioTarifacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaixasHorarioTarifacao_ConfiguracoesTarifacao_ConfiguracaoTarifacaoId",
                        column: x => x.ConfiguracaoTarifacaoId,
                        principalTable: "ConfiguracoesTarifacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaixasHorarioTarifacao_ConfiguracaoTarifacaoId_DiaSemana_HoraInicio_HoraFim",
                table: "FaixasHorarioTarifacao",
                columns: new[] { "ConfiguracaoTarifacaoId", "DiaSemana", "HoraInicio", "HoraFim" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaixasHorarioTarifacao");
        }
    }
}
