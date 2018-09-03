using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechPathology.Models
{
    [Table("ArticulationTestAnswers")]
    public class ArticulationTestAnswer : ModelBase
    {
        [ForeignKey(typeof(ArticulationTest)), NotNull]
        public long ArticulationTestId { get; set; }
        public int Number { get; set; }
        public bool? IsCorrect { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public ArticulationTest ArticulationTest { get; set; }
        public ArticulationTestAnswer() { }
        public ArticulationTestAnswer(int number, long articulationTestId)
        {
            this.Number = number;
            this.ArticulationTestId = articulationTestId;
        }
    }
}
