using SQLite;
using System;

namespace SpeechPathology.Models
{
    [Table("AgeCalculation")]
    public class AgeCalculation : ModelBase
    {
        public DateTime BirthDate { get; set; }
        public DateTime TestDate { get; set; }
        public int AgeInYears { get; set; }

        [Indexed]
        public string LanguageCode { get; set; }

        public readonly string LanguageSkillsBase = "languageskills_";
        public readonly string SpeechSoundsBase = "speechsounds_";
    }
}
