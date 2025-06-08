using Microsoft.AspNetCore.Mvc;
using QuidditchTrip.Models;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Configuration;

public static class TeamEndpointConfig
{
    public static IEndpointRouteBuilder RegisterTeamEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("teams/{key:int}",
            async (ITeamService teamService, int key)
            => await teamService.GetTeamByKey(key));

        app.MapGet("teams",
            async (ITeamService teamService)
            => await teamService.GetTeams());

        app.MapPost("teams/create/{name}",
            async (ITeamService teamService, string name)
            => await teamService.CreateTeam(name));

        app.MapPost("teams/{key:int}",
            async (ITeamService teamSerivce, int key, [FromBody] Team team)
            => await teamSerivce.SaveTeam(team));

        app.MapPost("teams/{key:int}/score/{value:int}",
            async (ITeamService teamService, int key, int value)
            => await teamService.Score(key, value));

        app.MapPost("teams/{key:int}/delete",
            async (ITeamService teamService, int key)
            => await teamService.DeleteTeam(key));

        return app;
    }
}
