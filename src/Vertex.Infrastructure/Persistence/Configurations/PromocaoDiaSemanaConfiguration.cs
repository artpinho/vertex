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
    public class PromocaoDiaSemanaConfiguration
        : IEntityTypeConfiguration<PromocaoDiaSemana>
    {
        public void Configure(
            EntityTypeBuilder<PromocaoDiaSemana> builder)
        {
            builder.ToTable("PromocoesDiasSemana");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PromocaoId)
                .IsRequired();

            builder.Property(x => x.DiaSemana)
                .IsRequired();

            builder.HasOne<Promocao>()
                .WithMany()
                .HasForeignKey(x => x.PromocaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.PromocaoId,
                x.DiaSemana
            })
            .IsUnique();
        }
    }
}
