using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.Auth
{
    public sealed record ComputerAuthenticationResponse(
    Guid ComputadorId,
    bool Authenticated,
    string AccessToken,
    string TokenType,
    int ExpiresIn);
}
