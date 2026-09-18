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
    public class CarteiraClienteConfiguration
        : IEntityTypeConfiguration<CarteiraCliente>
    {
        public void Configure(
            EntityTypeBuilder<CarteiraCliente> builder)
        {
            builder.ToTable("CarteirasClientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Saldo)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasIndex(x => x.ClienteId)
                .IsUnique();

            builder.HasOne<Cliente>()
                .WithOne()
                .HasForeignKey<CarteiraCliente>(
                    x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
