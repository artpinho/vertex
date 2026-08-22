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
    public sealed class ComputadorCredentialConfiguration
    : IEntityTypeConfiguration<ComputadorCredential>
    {
        public void Configure(
            EntityTypeBuilder<ComputadorCredential> builder)
        {
            builder.ToTable("ComputadorCredentials");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SecretHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Property(x => x.DataRevogacao);

            builder.HasIndex(x => x.ClientId)
                .IsUnique();

            builder.HasIndex(x => x.ComputadorId)
                .IsUnique();

            builder.HasOne<Computador>()
                .WithOne(x => x.Credential)
                .HasForeignKey<ComputadorCredential>(
                    x => x.ComputadorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
