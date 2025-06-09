using Microsoft.EntityFrameworkCore;
using QuidditchTrip.API.Configuration;
using QuidditchTrip.API.Configuration.Database;
using QuidditchTrip.API.Services;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API;
public class Program
{
    public static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("connectionstrings.json", optional: false, reloadOnChange: true)
                .Build();

        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddConfiguration(config);

        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string \'DefaultConnection\' not found.");

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<QuidditchContext>(
                options => options.UseNpgsql(connectionString));
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.RegisterEndpoints();
        app.Run();
    }
}
