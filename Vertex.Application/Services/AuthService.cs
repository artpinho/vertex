using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;
using Vertex.Infrastructure.Data;
using System.Security.Claims;
using Vertex.Domain.Entities;


namespace Vertex.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, PasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == request.Username);

            if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
                throw new Exception("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token
            };
        }
        private string GenerateJwtToken(Customer customer)
        {
            var keyValue = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(keyValue))
                throw new Exception("Chave JWT não está configurada.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyValue)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customer.Username),
                new Claim("CustomerId", customer.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }            
    }
}
