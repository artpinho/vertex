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
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;

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

        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(secret),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize);

        return string.Join(
            ".",
            "pbkdf2",
            Iterations,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash));
    }

    public bool VerifySecret(
        string secret,
        string hash)
    {
        if (string.IsNullOrWhiteSpace(secret) ||
            string.IsNullOrWhiteSpace(hash))
        {
            return false;
        }

        var parts = hash.Split('.');

        if (parts.Length != 4 ||
            parts[0] != "pbkdf2")
        {
            return false;
        }

        if (!int.TryParse(
                parts[1],
                out var iterations))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);

            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(secret),
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(
                actualHash,
                expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}