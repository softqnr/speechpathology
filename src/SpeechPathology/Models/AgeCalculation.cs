using SQLite;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Models
{
    [Table("AgeCalculation")]
    public class AgeCalculation : ModelBase
    {
        [Indexed]
        public string LSFile { get; set; }
    }
}
