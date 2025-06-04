namespace QuidditchTrip.Models
{
    public class Team : BaseModel
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int GameKey { get; set; }
    }
}
