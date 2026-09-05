using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vertex.Api.Security;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;
using Vertex.Application.Clients.Commands.AlterarStatus;
using Vertex.Application.Clients.Commands.AtualizarCliente;
using Vertex.Application.Clients.Commands.CriarCliente;
using Vertex.Application.Clients.Queries;
using Vertex.Application.Computers.Commands.AlterarStatus;
using Vertex.Application.Computers.Commands.AtualizarComputador;
using Vertex.Application.Computers.Commands.ProcessarHeartbeat;
using Vertex.Application.Computers.Commands.ProvisionarCredential;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Computers.Commands.RotacionarCredential;
using Vertex.Application.Computers.Queries;
using Vertex.Application.Sessions.Commands.EncerrarSessao;
using Vertex.Application.Sessions.Commands.IniciarSessao;
using Vertex.Application.Sessions.Queries;
using Vertex.Application.Stations.Commands.AlterarStatus;
using Vertex.Application.Stations.Commands.AssociarComputador;
using Vertex.Application.Stations.Commands.CriarEstacao;
using Vertex.Application.Stations.Queries;
using Vertex.Infrastructure;
using Vertex.Infrastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Informe somente o token JWT, sem o prefixo Bearer."
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

builder.Services.AddScoped<RegistrarComputadorHandler>();
builder.Services.AddScoped<ListarComputadoresHandler>();
builder.Services.AddScoped<ObterComputadorHandler>();
builder.Services.AddScoped<ProcessarHeartbeatHandler>();
builder.Services.AddScoped<ProvisionarComputadorCredentialHandler>();
builder.Services.AddScoped<RotacionarComputadorCredentialHandler>();
builder.Services.AddScoped<CriarEstacaoHandler>();
builder.Services.AddScoped<ListarEstacoesHandler>();
builder.Services.AddScoped<ObterEstacaoHandler>();
builder.Services.AddScoped<AssociarComputadorHandler>();
builder.Services.AddScoped<AlterarStatusEstacaoHandler>();
builder.Services.AddScoped<IniciarSessaoHandler>();
builder.Services.AddScoped<EncerrarSessaoHandler>();
builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<ObterClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<AlterarStatusClienteHandler>();
builder.Services.AddScoped<ListarSessoesHandler>();
builder.Services.AddScoped<ObterSessaoHandler>();
builder.Services.AddScoped<AtualizarComputadorHandler>();
builder.Services.AddScoped<AlterarStatusComputadorHandler>();

var jwtKey =
    builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "Jwt:Key não configurado.");

var jwtKeyId =
    builder.Configuration["Jwt:KeyId"]
    ?? throw new InvalidOperationException(
        "Jwt:KeyId não configurado.");

var jwtIssuer =
    builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "Jwt:Issuer não configurado.");

var jwtAudience =
    builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "Jwt:Audience não configurado.");

builder.Services.AddCors(options =>
{
    options.AddPolicy("SwaggerPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5250",
                "https://localhost:7098")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey))
                    {
                        KeyId = jwtKeyId
                    },

                ClockSkew = TimeSpan.FromSeconds(30)
            };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentComputer,CurrentComputer>();
builder.Services.AddScoped<IEstacaoRepository,EstacaoRepository>();
builder.Services.AddScoped<ISessaoRepository, SessaoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("SwaggerPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
