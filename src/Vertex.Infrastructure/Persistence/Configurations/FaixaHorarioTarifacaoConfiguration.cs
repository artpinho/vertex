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
    public class FaixaHorarioTarifacaoConfiguration
        : IEntityTypeConfiguration<FaixaHorarioTarifacao>
    {
        public void Configure(
            EntityTypeBuilder<FaixaHorarioTarifacao> builder)
        {
            builder.ToTable("FaixasHorarioTarifacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConfiguracaoTarifacaoId)
                .IsRequired();

            builder.Property(x => x.DiaSemana)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();

            builder.Property(x => x.ValorHora)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder.HasOne<ConfiguracaoTarifacao>()
                .WithMany()
                .HasForeignKey(x => x.ConfiguracaoTarifacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ConfiguracaoTarifacaoId,
                x.DiaSemana,
                x.HoraInicio,
                x.HoraFim
            })
            .IsUnique();
        }
    }
}
