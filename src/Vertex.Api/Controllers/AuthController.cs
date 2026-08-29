using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Abstractions.Security;
using Vertex.Contracts.Auth;

namespace Vertex.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IComputerAuthenticator _authenticator;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICurrentComputer _currentComputer;

    public AuthController(
     IComputerAuthenticator authenticator,
     IJwtTokenService jwtTokenService,
     ICurrentComputer currentComputer)
    {
        _authenticator = authenticator;
        _jwtTokenService = jwtTokenService;
        _currentComputer = currentComputer;
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

        var accessToken = _jwtTokenService.GenerateToken(
            computadorId.Value,
            request.ClientId);

        return Ok(
            new ComputerAuthenticationResponse(
                computadorId.Value,
                true,
                accessToken,
                "Bearer",
                3600));
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Me()
    {
        return Ok(new
        {
            computadorId = _currentComputer.ComputadorId,
            clientId = _currentComputer.ClientId,
            tipo = "computador"
        });
    }
}
