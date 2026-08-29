using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Abstractions.Security;

public interface ICurrentComputer
{
    Guid ComputadorId { get; }

    string ClientId { get; }
}