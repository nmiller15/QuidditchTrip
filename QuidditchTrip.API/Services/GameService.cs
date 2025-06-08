using Microsoft.EntityFrameworkCore;
using QuidditchTrip.API.Configuration.Database;
using QuidditchTrip.Models;
using QuidditchTrip.Models.Interfaces;

namespace QuidditchTrip.API.Services;

public class GameService : IGameService
{
    private readonly QuidditchContext _context;

    public GameService(QuidditchContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<Game>> GetGameByKey(int gameKey)
    {
        var game = await _context.Games
            .Include(g => g.Teams)
            .SingleAsync(g => g.GameKey == gameKey);
        if (game == null) { return ServiceResponse<Game>.Failure("No game found with the provided key."); }
        return ServiceResponse<Game>.Success(game);
    }

    public async Task<ServiceResponse<List<Game>>> GetGames()
    {
        var game = await _context.Games
            .Include(g => g.Teams)
            .ToListAsync();
        return ServiceResponse<List<Game>>.Success(game);
    }

    public async Task<ServiceResponse<Game>> CreateGame(int teamKey1, int teamKey2)
    {
        try
        {
            var team1 = await _context.Teams
                .SingleAsync(t => t.TeamKey == teamKey1);
            var team2 = await _context.Teams
                .SingleAsync(t => t.TeamKey == teamKey2);

            if (team1 == null || team2 == null) { return ServiceResponse<Game>.Failure("One of the teams could not be found."); }

            var activeIndex = new Random().Next(0, 2);
            if (activeIndex < 1)
            {
                team1.IsActive = true;
                team2.IsActive = false;
            }
            else
            {
                team1.IsActive = false;
                team2.IsActive = true;
            }

            var game = new Game
            {
                GameStartDateTime = DateTime.UtcNow,
                IsActive = true,
                Teams = new List<Team> { team1, team2 }
            };

            _context.Games.Add(game);
            var changedGames = await _context.SaveChangesAsync();
            if (changedGames < 1) { return ServiceResponse<Game>.Failure("Failed to create game."); }

            team1.GameKey = game.GameKey;
            team1.Game = game;
            team2.GameKey = game.GameKey;
            team2.Game = game;

            _context.Teams.Update(team1);
            _context.Teams.Update(team2);
            var changedTeams = await _context.SaveChangesAsync();
            if (changedTeams < 2) { return ServiceResponse<Game>.Failure("Failed to update teams with game key."); }

            return ServiceResponse<Game>.Success(game);
        }
        catch (Exception ex)
        {
            return ServiceResponse<Game>.Failure($"An error occurred while creating the game: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> SaveGame(Game game)
    {
        _context.Add(game);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<bool>.Failure("Failed to save game."); }
        return ServiceResponse<bool>.Success(true);
    }

    public async Task<ServiceResponse<bool>> DeleteGame(int gameKey)
    {
        _context.Add(new Game { GameKey = gameKey });
        _context.Remove(new Game { GameKey = gameKey });
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<bool>.Failure("Failed to delete game."); }
        return ServiceResponse<bool>.Success(true);
    }

    public async Task<ServiceResponse<Game>> ChangeActiveTeam(int gameKey)
    {
        Console.WriteLine($"Changing active team for game with key: {gameKey}");
        var game = await _context.Games
            .Include(g => g.Teams)
            .SingleAsync(g => g.GameKey == gameKey);
        if (game == null) { return ServiceResponse<Game>.Failure("No game found with the provided key."); }

        foreach (var team in game.Teams)
        {
            team.IsActive = !team.IsActive;
            _context.Teams.Update(team);
        }

        var changedTeams = await _context.SaveChangesAsync();
        if (changedTeams < 2) { return ServiceResponse<Game>.Failure("Failed to change active team."); }

        return ServiceResponse<Game>.Success(game);
    }

    public async Task<ServiceResponse<Game>> SetGameInactive(int gameKey)
    {
        var game = await _context.Games
            .Include(g => g.Teams)
            .SingleAsync(g => g.GameKey == gameKey);
        if (game == null) { return ServiceResponse<Game>.Failure("No game found with the provided key."); }

        game.IsActive = false;
        _context.Games.Update(game);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<Game>.Failure("Failed to set game inactive."); }

        return ServiceResponse<Game>.Success(game);
    }

    public async Task<ServiceResponse<Game>> EndGame(int gameKey)
    {
        var game = await _context.Games
            .SingleAsync(g => g.GameKey == gameKey);
        if (game == null) { return ServiceResponse<Game>.Failure("No game found with the provided key."); }

        game.GameEndDateTime = DateTime.UtcNow;
        game.IsActive = false;
        _context.Games.Update(game);
        var changedRows = await _context.SaveChangesAsync();
        if (changedRows < 1) { return ServiceResponse<Game>.Failure("Failed to finish game."); }

        foreach (var team in game.Teams)
        {
            team.IsActive = false;
            _context.Teams.Update(team);
        }

        var changedTeams = await _context.SaveChangesAsync();
        if (changedRows < 2) { return ServiceResponse<Game>.Failure("Failed to update teams after finishing game."); }

        return ServiceResponse<Game>.Success(game);
    }
}
