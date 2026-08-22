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
    public class SessaoConfiguration
    : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            builder.ToTable("Sessoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.EstacaoId)
                .IsRequired();

            builder.Property(x => x.Inicio)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasIndex(x => x.ClienteId);

            builder.HasIndex(x => x.EstacaoId);

            builder.HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Estacao>()
                .WithMany()
                .HasForeignKey(x => x.EstacaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
