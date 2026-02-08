using GYMIND.API.Interfaces;
using GYMIND.API.GYMIND.Application.Service;
using GYMIND.API.GYMIND.Application.Services;
using GYMIND.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<DatabaseConnection>();

builder.Services.AddDbContext<SupabaseDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Supabase"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure();
        });
});


// Register Controllers
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

//Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
