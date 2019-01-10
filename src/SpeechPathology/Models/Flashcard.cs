using SQLite;

namespace SpeechPathology.Models
{
    [Table("Flashcards")]
    public class Flashcard : ModelBase
    {
        public string Sound { get; set; }
  
        public string Text { get; set; }
        [Indexed]
        public string SoundPosition { get; set; }

        public string ImageFile { get; set; }

        public string SoundFile { get; set; }
        [Indexed]
        public string LanguageCode { get; set; }
    }
}
