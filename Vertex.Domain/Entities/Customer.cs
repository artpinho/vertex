using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.ValueObjects;

namespace Vertex.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Document { get; set; } 

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; } 

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public Address? Address { get; set; }

        public byte[]? Photo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
