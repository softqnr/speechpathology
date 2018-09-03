using SQLite;

namespace SpeechPathology.Models
{
    [Table("ArticulationTests")]
    public class ArticulationTest : ModelBase
    {
        public string Sound { get; set; }
  
        public string Text { get; set; }

        public string Position { get; set; }

        public string Image { get; set; }
    }
}
