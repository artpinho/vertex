using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vertex.Domain.Entities;

namespace Vertex.Infrastructure.Persistence.Configurations
{
    public class EstacaoConfiguration
    : IEntityTypeConfiguration<Estacao>
    {
        public void Configure(EntityTypeBuilder<Estacao> builder)
        {
            builder.ToTable("Estacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Ativa)
                .IsRequired();

            builder.HasIndex(x => x.Numero)
                .IsUnique();

            builder.HasOne<Computador>()
                .WithOne()
                .HasForeignKey<Estacao>(x => x.ComputadorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
