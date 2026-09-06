using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarPromocoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promocoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PercentualDesconto = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    ValorDescontoHora = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    TodosTiposMaquina = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promocoes_Ativo_DataInicio_DataFim_Prioridade",
                table: "Promocoes",
                columns: new[] { "Ativo", "DataInicio", "DataFim", "Prioridade" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promocoes");
        }
    }
}
