using SQLite;

namespace SpeechPathology.Models
{
    [Table("ArticulationTests")]
    public class Flashcard : ModelBase
    {
        public string Sound { get; set; }
  
        public string Text { get; set; }
        [Indexed]
        public string SoundPosition { get; set; }

        public string Imagefile { get; set; }

        public string Soundfile { get; set; }
    }
}
