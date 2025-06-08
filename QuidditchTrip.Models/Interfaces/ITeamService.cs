namespace QuidditchTrip.Models.Interfaces;

public interface ITeamService
{
    Task<ServiceResponse<Team>> GetTeamByKey(int teamKey);
    Task<ServiceResponse<List<Team>>> GetTeams();
    Task<ServiceResponse<Team>> CreateTeam(string name);
    Task<ServiceResponse<bool>> SaveTeam(Team team);
    Task<ServiceResponse<int>> Score(int teamKey, int value);
    Task<ServiceResponse<bool>> DeleteTeam(int teamKey);
}
