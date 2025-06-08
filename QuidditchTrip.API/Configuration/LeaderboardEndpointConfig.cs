using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Configuration;

public static class LeaderboardEndpointConfig
{
    public static IEndpointRouteBuilder RegisterLeaderboardEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("leaderboard",
            async (ILeaderboardService leaderboardService, int count = 10)
            => await leaderboardService.GetLeaderboard(count));

        return app;
    }
}
