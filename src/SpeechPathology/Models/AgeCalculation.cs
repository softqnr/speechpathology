using SQLite;

namespace SpeechPathology.Models
{
    [Table("AgeCalculation")]
    public class AgeCalculation : ModelBase
    {
        [Indexed]
        public string LanguageSkillsFile { get; set; }
    }
}
