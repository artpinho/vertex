using Vertex.Application.Computers.Commands.ProcessarHeartbeat;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Computers.Queries;
using Vertex.Infrastructure;
using Vertex.Application.Computers.Commands.ProvisionarCredential;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RegistrarComputadorHandler>();
builder.Services.AddScoped<ListarComputadoresHandler>();
builder.Services.AddScoped<ObterComputadorHandler>();
builder.Services.AddScoped<ProcessarHeartbeatHandler>();
builder.Services.AddScoped<ProvisionarComputadorCredentialHandler>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
