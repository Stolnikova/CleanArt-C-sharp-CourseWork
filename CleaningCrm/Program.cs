using CleaningCrm.Data;
using CleaningCrm.Mappers;
using CleaningCrm.Repositories;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services;
using CleaningCrm.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();

// Mappers
builder.Services.AddSingleton<UserMapper>();

// Swagger / Scalar
builder.Services.AddOpenApi();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o => o.WithTitle("Cleaning CRM API"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();