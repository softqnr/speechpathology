using SQLite;

namespace SpeechPathology.Models
{
    [Table("Worksheets")]
    public class Worksheet : ModelBase
    {
        [Indexed]
        public string Sound { get; set; }
  
        public string File { get; set; }

        public string LanguageCode { get; set; }
    }
}
