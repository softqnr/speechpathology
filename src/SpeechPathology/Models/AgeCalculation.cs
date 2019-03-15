﻿using SQLite;

namespace SpeechPathology.Models
{
    [Table("AgeCalculations")]
    public class AgeCalculation : ModelBase
    {
        [Indexed]
        public int AgeInYears { get; set; }

        public string LanguageSkillsFile { get; set; }

        public string SpeechSoundsFile { get; set; }
    }
}
