using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriarCarteiraCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarteirasClientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteirasClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarteirasClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesCarteira",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarteiraClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    TipoPagamento = table.Column<int>(type: "int", nullable: true),
                    SessaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesCarteira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacoesCarteira_CarteirasClientes_CarteiraClienteId",
                        column: x => x.CarteiraClienteId,
                        principalTable: "CarteirasClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarteirasClientes_ClienteId",
                table: "CarteirasClientes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesCarteira_CarteiraClienteId",
                table: "MovimentacoesCarteira",
                column: "CarteiraClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesCarteira_Data",
                table: "MovimentacoesCarteira",
                column: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacoesCarteira");

            migrationBuilder.DropTable(
                name: "CarteirasClientes");
        }
    }
}
