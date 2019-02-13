using SQLite;
using System;

namespace SpeechPathology.Models
{
    [Table("AgeCalculation")]
    public class AgeCalculation : ModelBase
    {
        public DateTime BirthDate { get; set; }
        public DateTime TestDate { get; set; }
        [Indexed]
        public string LanguageCode { get; set; }
    }
}
