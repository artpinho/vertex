using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Security;

namespace Vertex.Application.Tests.Fakes;

public sealed class FakeComputerCredentialGenerator
    : IComputerCredentialGenerator
{
    public string GenerateClientId()
    {
        return "vtx_test";
    }

    public string GenerateClientSecret()
    {
        return "secret-test";
    }

    public string HashSecret(string secret)
    {
        return $"hash:{secret}";
    }

    public bool VerifySecret(
        string secret,
        string hash)
    {
        return hash == $"hash:{secret}";
    }
}