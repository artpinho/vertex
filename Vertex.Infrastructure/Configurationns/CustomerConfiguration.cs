using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Infrastructure.Configurationns
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.Property(c => c.Name).HasMaxLength(150);

            entity.Property(c => c.Username).HasMaxLength(50);

            entity.Property(c => c.Email).HasMaxLength(150);

            entity.Property(c => c.PhoneNumber).HasMaxLength(20);

            entity.Property(c => c.Document).HasMaxLength(20);

            entity.OwnsOne(c => c.Address);
        }
    }
}
