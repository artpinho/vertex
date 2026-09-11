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
    public class PromocaoFaixaHorarioConfiguration
        : IEntityTypeConfiguration<PromocaoFaixaHorario>
    {
        public void Configure(
            EntityTypeBuilder<PromocaoFaixaHorario> builder)
        {
            builder.ToTable("PromocoesFaixasHorario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PromocaoId)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();

            builder.HasOne<Promocao>()
                .WithMany()
                .HasForeignKey(x => x.PromocaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.PromocaoId,
                x.HoraInicio,
                x.HoraFim
            })
            .IsUnique();
        }
    }
}
