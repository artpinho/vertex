using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Abstractions.Security;

public interface IComputerCredentialGenerator
{
    string GenerateClientId();

    string GenerateClientSecret();

    string HashSecret(string secret);
}
