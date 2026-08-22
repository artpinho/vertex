using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Computers.Commands.ProvisionarCredential
{
    public sealed record ProvisionarComputadorCredentialResponse(
        Guid ComputadorId,
        string ClientId,
        string ClientSecret);
}
