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
    public class ConfiguracaoTarifacaoConfiguration
    : IEntityTypeConfiguration<ConfiguracaoTarifacao>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoTarifacao> builder)
        {
            builder.ToTable("ConfiguracoesTarifacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.TipoMaquinaId)
                .IsRequired();

            builder.Property(x => x.ValorHora)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataFim)
                .IsRequired(false);

            builder.Property(x => x.Prioridade)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder.HasOne<TipoMaquina>()
                .WithMany()
                .HasForeignKey(x => x.TipoMaquinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.TipoMaquinaId,
                x.Ativo,
                x.DataInicio
            });
        }
    }
}
