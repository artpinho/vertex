using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Abstractions.Security;
using Vertex.Contracts.Auth;

namespace Vertex.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IComputerAuthenticator _authenticator;

    public AuthController(
        IComputerAuthenticator authenticator)
    {
        _authenticator = authenticator;
    }

    [HttpPost("computers")]
    [ProducesResponseType(
        typeof(ComputerAuthenticationResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<
        ActionResult<ComputerAuthenticationResponse>>
        AuthenticateComputer(
            [FromBody] ComputerAuthenticationRequest request,
            CancellationToken cancellationToken)
    {
        var computadorId =
            await _authenticator.AuthenticateAsync(
                request.ClientId,
                request.ClientSecret,
                cancellationToken);

        if (computadorId is null)
        {
            return Unauthorized(new
            {
                message = "Credenciais inválidas."
            });
        }

        return Ok(
            new ComputerAuthenticationResponse(
                computadorId.Value,
                true));
    }
}
