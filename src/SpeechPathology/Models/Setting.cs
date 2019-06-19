using SQLite;

namespace SpeechPathology.Models
{
    [Table("Settings")]
    public class Setting : ModelBase
    {
        [Indexed]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
