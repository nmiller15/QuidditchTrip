namespace QuidditchTrip.Models.Interfaces;

public interface ILeaderboardService
{
    Task<ServiceResponse<List<LeaderboardEntry>>> GetLeaderboard(int count = 10);
}
