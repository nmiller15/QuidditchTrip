using Microsoft.EntityFrameworkCore;
using QuidditchTrip.API.Configuration.Database;
using QuidditchTrip.Models;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly QuidditchContext _context;

    public LeaderboardService(QuidditchContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<LeaderboardEntry>>> GetLeaderboard(int count = 10)
    {
        var teams = await _context.Teams
            .OrderByDescending(t => t.Score)
            .Include(t => t.Game)
            .Where(t => t.GameKey != null)
            .Take(count)
            .ToListAsync();
        if (teams == null) return ServiceResponse<List<LeaderboardEntry>>.Failure("Unable to retrieve leaderboard.");

        var leaderboard = teams.ConvertAll(t =>
                new LeaderboardEntry
                {
                    Name = t.Name,
                    Score = t.Score,
                    Date = t.Game.GameEndDateTime is null
                        ? t.CreatedDateTime
                        : t.Game.GameEndDateTime.Value
                });

        return ServiceResponse<List<LeaderboardEntry>>.Success(leaderboard);
    }
}
