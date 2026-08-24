using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.Auth
{
    public sealed record ComputerAuthenticationRequest(
        string ClientId,
        string ClientSecret);
}
