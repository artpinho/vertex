using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Security;

namespace Vertex.Infrastructure.Security;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(
        Guid computadorId,
        string clientId)
    {
        var issuer =
            _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "Jwt:Issuer não configurado.");

        var audience =
            _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "Jwt:Audience não configurado.");

        var key =
            _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "Jwt:Key não configurado.");

        var expirationMinutes = int.TryParse(
            _configuration["Jwt:ExpirationMinutes"],
            out var configuredExpiration)
                ? configuredExpiration
                : 60;

        var keyId =_configuration["Jwt:KeyId"] ?? throw new InvalidOperationException("Jwt:KeyId não configurado.");
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key))
            {
                KeyId = keyId
            };

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                computadorId.ToString()),

            new Claim(
                "computadorId",
                computadorId.ToString()),

            new Claim(
                "clientId",
                clientId),

            new Claim(
                "tipo",
                "computador")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}