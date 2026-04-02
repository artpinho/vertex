using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Vertex.Infrastructure.Data;
using Vertex.Application.Interfaces;
using Vertex.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<IStationService, StationService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
