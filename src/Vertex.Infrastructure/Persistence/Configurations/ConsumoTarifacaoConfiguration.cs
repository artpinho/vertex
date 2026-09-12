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
    public class ConsumoTarifacaoConfiguration
        : IEntityTypeConfiguration<ConsumoTarifacao>
    {
        public void Configure(EntityTypeBuilder<ConsumoTarifacao> builder)
        {
            builder.ToTable("ConsumosTarifacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ValorHora)
                .HasPrecision(18, 2);

            builder.Property(x => x.Desconto)
                .HasPrecision(18, 2);

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2);

            builder.HasOne<Sessao>()
                .WithMany()
                .HasForeignKey(x => x.SessaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ConfiguracaoTarifacao>()
                .WithMany()
                .HasForeignKey(x => x.ConfiguracaoTarifacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Promocao>()
                .WithMany()
                .HasForeignKey(x => x.PromocaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.SessaoId,
                x.Inicio,
                x.Fim
            })
            .IsUnique();
        }
    }
}
