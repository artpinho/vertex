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
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CPF)
                .HasMaxLength(14);

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.Telefone)
                .HasMaxLength(20);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();
        }
    }
}
