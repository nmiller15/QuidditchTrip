namespace QuidditchTrip.Models;

public class Game
{
    public int GameKey { get; set; }
    public DateTime GameStartDateTime { get; set; }
    public DateTime? GameEndDateTime { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFinished => GameEndDateTime != null;

    public List<Team> Teams { get; set; } = new List<Team>();
}
