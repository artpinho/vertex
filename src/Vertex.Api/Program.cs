using Vertex.Application.Computers.Commands.ProcessarHeartbeat;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Computers.Queries;
using Vertex.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddScoped<RegistrarComputadorHandler>();
builder.Services.AddScoped<ListarComputadoresHandler>();
builder.Services.AddScoped<ObterComputadorHandler>();
builder.Services.AddScoped<ProcessarHeartbeatHandler>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
