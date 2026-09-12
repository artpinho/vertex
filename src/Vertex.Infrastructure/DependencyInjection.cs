using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;
using Vertex.Application.Promotions.Commands.AssociarPromocaoDiaSemana;
using Vertex.Application.Promotions.Commands.AssociarPromocaoFaixaHorario;
using Vertex.Application.Promotions.Commands.AtualizarPromocaoFaixaHorario;
using Vertex.Application.Promotions.Commands.RemoverPromocaoDiaSemana;
using Vertex.Application.Promotions.Commands.RemoverPromocaoFaixaHorario;
using Vertex.Application.Promotions.Queries.ListarDiasSemanaPromocao;
using Vertex.Application.Promotions.Queries.ListarFaixasHorarioPromocao;
using Vertex.Application.Tariffing.Commands.CalcularTarifacao;
using Vertex.Application.Tariffing.Services;
using Vertex.Infrastructure.Persistence.Context;
using Vertex.Infrastructure.Persistence.Repositories;
using Vertex.Infrastructure.Security;
using Vertex.Infrastructure.Tariffing;

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
            services.AddScoped<ITarifacaoRepository, ConfiguracaoTarifacaoRepository>();
            services.AddScoped<IPromocaoDiaSemanaRepository, PromocaoDiaSemanaRepository>();
            services.AddScoped<IPromocaoFaixaHorarioRepository, PromocaoFaixaHorarioRepository>();
            services.AddScoped<ITarifacaoProvider, TarifacaoProvider>();
            services.AddScoped<IMotorTarifacao, MotorTarifacao>();
            services.AddScoped<CalcularTarifacaoHandler>();

            return services;
        }
    }
}
