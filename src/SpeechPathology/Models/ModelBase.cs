using SQLite;

namespace SpeechPathology.Models
{
    public class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
    }
}
