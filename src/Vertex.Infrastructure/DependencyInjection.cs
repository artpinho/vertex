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

            return services;
        }
    }
}
