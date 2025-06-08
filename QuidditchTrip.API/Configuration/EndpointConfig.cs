namespace QuidditchTrip.API.Configuration;

public static class EndpointConfig
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello World!");
        app.RegisterGameEndpoints();
        app.RegisterTeamEndpoints();
        app.RegisterLeaderboardEndpoints();
    }
}
