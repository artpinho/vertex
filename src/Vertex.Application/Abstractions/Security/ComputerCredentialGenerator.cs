using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Security;

namespace Vertex.Infrastructure.Security;

public sealed class ComputerCredentialGenerator
    : IComputerCredentialGenerator
{
    public string GenerateClientId()
    {
        return $"vtx_{Convert.ToHexString(
            RandomNumberGenerator.GetBytes(16))
            .ToLowerInvariant()}";
    }

    public string GenerateClientSecret()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(32));
    }

    public string HashSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);

        var bytes = SHA256.HashData(
            Encoding.UTF8.GetBytes(secret));

        return Convert.ToHexString(bytes)
            .ToLowerInvariant();
    }
}