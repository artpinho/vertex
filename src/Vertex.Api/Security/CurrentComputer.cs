using Vertex.Application.Abstractions.Security;

namespace Vertex.Api.Security;

public sealed class CurrentComputer : ICurrentComputer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentComputer(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid ComputadorId
    {
        get
        {
            var value =
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst("computadorId")
                    ?.Value;

            if (!Guid.TryParse(value, out var computadorId))
            {
                throw new InvalidOperationException(
                    "Computador autenticado não encontrado.");
            }

            return computadorId;
        }
    }

    public string ClientId
    {
        get
        {
            return _httpContextAccessor.HttpContext?
                       .User
                       .FindFirst("clientId")
                       ?.Value
                   ?? throw new InvalidOperationException(
                       "ClientId autenticado não encontrado.");
        }
    }
}