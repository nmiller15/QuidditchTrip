using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuidditchTrip.Models
{
    public class Team
    {
        public int TeamKey { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? GameKey { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
    }
}
