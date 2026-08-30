using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vertex.Infrastructure.Persistence.Context;
using Vertex.Infrastructure.Persistence.Repositories;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;
using Vertex.Infrastructure.Security;

namespace Vertex.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(
                "VertexConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "A connection string 'VertexConnection' não foi configurada.");
            }

            services.AddDbContext<VertexDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IComputadorRepository, ComputadorRepository>();
            services.AddScoped<IComputadorCredentialRepository, ComputadorCredentialRepository>();
            services.AddScoped<IComputerCredentialGenerator, ComputerCredentialGenerator>();
            services.AddScoped<IComputerAuthenticator, ComputerAuthenticator>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IEstacaoRepository, EstacaoRepository>();

            return services;
        }
    }
}
