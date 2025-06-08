using Microsoft.EntityFrameworkCore;
using QuidditchTrip.API.Configuration.Database;
using QuidditchTrip.Models;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Services;

public class TeamService : ITeamService
{
    private readonly QuidditchContext _context;

    public TeamService(QuidditchContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<Team>> GetTeamByKey(int teamKey)
    {
        var team = await _context.Teams
            .Include(t => t.Game)
            .SingleAsync(g => g.TeamKey == teamKey);
        if (team == null) { return ServiceResponse<Team>.Failure("No team found with the provided key."); }
        return ServiceResponse<Team>.Success(team);
    }

    public async Task<ServiceResponse<List<Team>>> GetTeams()
    {
        var team = await _context.Teams
            .Include(t => t.Game)
            .ToListAsync();
        return ServiceResponse<List<Team>>.Success(team);
    }

    public async Task<ServiceResponse<Team>> CreateTeam(string name)
    {
        var team = new Team()
        {
            Name = name,
            Score = 0,
            CreatedDateTime = DateTime.UtcNow
        };
        _context.Teams.Add(team);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<Team>.Failure("Failed to create team."); }
        return ServiceResponse<Team>.Success(team);
    }

    public async Task<ServiceResponse<bool>> SaveTeam(Team team)
    {
        _context.Add(team);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<bool>.Failure("Failed to save team."); }
        return ServiceResponse<bool>.Success(true);
    }

    public async Task<ServiceResponse<int>> Score(int teamKey, int value)
    {
        var team = await _context.Teams
            .SingleAsync(t => t.TeamKey == teamKey);

        if (!team.IsActive) { return ServiceResponse<int>.Failure("Can only score while active."); }
        if (team.Score + value >= 0)
        {
            team.Score += value;
        }
        _context.Update(team);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<int>.Failure("Failed to update score."); }
        return ServiceResponse<int>.Success(team.Score);
    }

    public async Task<ServiceResponse<bool>> DeleteTeam(int teamKey)
    {
        _context.Add(new Team { TeamKey = teamKey });
        _context.Remove(new Team { TeamKey = teamKey });
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<bool>.Failure("Failed to delete team."); }
        return ServiceResponse<bool>.Success(true);
    }
}
