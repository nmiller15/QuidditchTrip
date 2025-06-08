using Microsoft.AspNetCore.Mvc;
using QuidditchTrip.Models;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Configuration;

public static class GameEndpointConfig
{
    public static IEndpointRouteBuilder RegisterGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("games",
            async (IGameService gameService)
            => await gameService.GetGames());

        app.MapPost("games/teams/{teamKey1:int}/{teamKey2:int}",
            async (IGameService gameService, int teamKey1, int teamKey2)
            => await gameService.CreateGame(teamKey1, teamKey2));

        app.MapGet("games/{key:int}",
            async (IGameService gameService, int key)
            => await gameService.GetGameByKey(key));

        app.MapPost("games/{key:int}",
            async (IGameService gameService, int key, [FromBody] Game game)
            => await gameService.SaveGame(game));

        app.MapPost("games/{key:int}/change-active",
            async (IGameService gameService, int key)
            => await gameService.ChangeActiveTeam(key));

        app.MapPost("games/{key:int}/delete",
            async (IGameService gameService, int key)
            => await gameService.DeleteGame(key));

        app.MapPost("games/{key:int}/inactive",
            async (IGameService gameService, int key)
            => await gameService.SetGameInactive(key));

        app.MapPost("games/{key:int}/end",
            async (IGameService gameService, int key)
            => await gameService.EndGame(key));

        return app;
    }
}
