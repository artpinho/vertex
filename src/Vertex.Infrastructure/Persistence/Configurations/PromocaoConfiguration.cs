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
    public class PromocaoConfiguration : IEntityTypeConfiguration<Promocao>
    {
        public void Configure(EntityTypeBuilder<Promocao> builder)
        {
            builder.ToTable("Promocoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.PercentualDesconto)
                .HasPrecision(5, 2);

            builder.Property(x => x.ValorDescontoHora)
                .HasPrecision(18, 2);

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataFim);

            builder.Property(x => x.Prioridade)
                .IsRequired();

            builder.Property(x => x.TodosTiposMaquina)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.Ativo,
                x.DataInicio,
                x.DataFim,
                x.Prioridade
            });
        }
    }
}
