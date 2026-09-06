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
    public class PromocaoTipoMaquinaConfiguration
        : IEntityTypeConfiguration<PromocaoTipoMaquina>
    {
        public void Configure(
            EntityTypeBuilder<PromocaoTipoMaquina> builder)
        {
            builder.ToTable("PromocoesTiposMaquina");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PromocaoId)
                .IsRequired();

            builder.Property(x => x.TipoMaquinaId)
                .IsRequired();

            builder.HasOne<Promocao>()
                .WithMany()
                .HasForeignKey(x => x.PromocaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TipoMaquina>()
                .WithMany()
                .HasForeignKey(x => x.TipoMaquinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.PromocaoId,
                x.TipoMaquinaId
            })
            .IsUnique();
        }
    }
}
