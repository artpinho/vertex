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
    public class ComputadorConfiguration
    : IEntityTypeConfiguration<Computador>
    {
        public void Configure(EntityTypeBuilder<Computador> builder)
        {
            builder.ToTable("Computadores");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.HostName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Ip)
                .HasMaxLength(45);

            builder.Property(x => x.MacAddress)
                .HasMaxLength(17);

            builder.Property(x => x.SistemaOperacional)
                .HasMaxLength(100);

            builder.Property(x => x.ClienteVersao)
                .HasMaxLength(30);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.UltimoHeartbeat);

            builder.HasIndex(x => x.HostName)
            .IsUnique();

            builder.HasIndex(x => x.MacAddress)
                .IsUnique()
                .HasFilter("[MacAddress] IS NOT NULL");
        }
    }
}
