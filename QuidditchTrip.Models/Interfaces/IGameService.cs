namespace QuidditchTrip.Models.Interfaces;

public interface IGameService
{
    Task<ServiceResponse<Game>> GetGameByKey(int gameKey);
    Task<ServiceResponse<List<Game>>> GetGames();
    Task<ServiceResponse<Game>> CreateGame(int teamKey1, int teamKey2);
    Task<ServiceResponse<bool>> SaveGame(Game game);
    Task<ServiceResponse<Game>> ChangeActiveTeam(int gameKey);
    Task<ServiceResponse<Game>> SetGameInactive(int gameKey);
    Task<ServiceResponse<Game>> EndGame(int gameKey);
    Task<ServiceResponse<bool>> DeleteGame(int gameKey);
}
