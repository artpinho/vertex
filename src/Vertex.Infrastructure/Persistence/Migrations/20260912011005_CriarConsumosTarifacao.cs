using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarConsumosTarifacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumosTarifacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfiguracaoTarifacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumosTarifacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumosTarifacao_ConfiguracoesTarifacao_ConfiguracaoTarifacaoId",
                        column: x => x.ConfiguracaoTarifacaoId,
                        principalTable: "ConfiguracoesTarifacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumosTarifacao_Promocoes_PromocaoId",
                        column: x => x.PromocaoId,
                        principalTable: "Promocoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumosTarifacao_Sessoes_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "Sessoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumosTarifacao_ConfiguracaoTarifacaoId",
                table: "ConsumosTarifacao",
                column: "ConfiguracaoTarifacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumosTarifacao_PromocaoId",
                table: "ConsumosTarifacao",
                column: "PromocaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumosTarifacao_SessaoId_Inicio_Fim",
                table: "ConsumosTarifacao",
                columns: new[] { "SessaoId", "Inicio", "Fim" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumosTarifacao");
        }
    }
}
