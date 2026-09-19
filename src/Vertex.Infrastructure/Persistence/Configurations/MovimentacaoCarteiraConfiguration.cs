using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Infrastructure.Persistence.Configurations
{
    public class MovimentacaoCarteiraConfiguration
        : IEntityTypeConfiguration<MovimentacaoCarteira>
    {
        public void Configure(
            EntityTypeBuilder<MovimentacaoCarteira> builder)
        {
            builder.ToTable("MovimentacoesCarteira");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.TipoPagamento)
                .IsRequired(false);

            builder.HasIndex(x => x.CarteiraClienteId);

            builder.HasIndex(x => x.Data);

            builder.HasOne<CarteiraCliente>()
                .WithMany()
                .HasForeignKey(x => x.CarteiraClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SessaoId)
                .IsUnique()
                .HasFilter("[SessaoId] IS NOT NULL AND [Tipo] = 2");
        }
    }
}
