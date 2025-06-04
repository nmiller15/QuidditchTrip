namespace QuidditchTrip.Models
{
    public class Game : BaseModel
    {
        public TimeOnly TimeRemaining { get; set; } = new TimeOnly(0, 5, 0);
        public int ActiveTeamKey { get; set; }
        public int WinningTeamKey { get; set; }
        public DateTime GameStartDate { get; set; }
        public DateTime GameEndDate { get; set; }
    }
}
