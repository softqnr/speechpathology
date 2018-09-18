using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SpeechPathology.Models
{
    [Table("ArticulationTestExamAnswers")]
    public class ArticulationTestExamAnswer : ModelBase
    {
        public int Number { get; set; }
        public bool? IsCorrect { get; set; }

        [Indexed]
        [ForeignKey(typeof(ArticulationTestExam))]
        public long ArticulationTestExamId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public ArticulationTestExam ArticulationTestExam { get; set; }

        [Indexed]
        [ForeignKey(typeof(ArticulationTest))]
        public long ArticulationTestId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public ArticulationTest ArticulationTest { get; set; }

        public ArticulationTestExamAnswer() { }
        public ArticulationTestExamAnswer(ArticulationTestExam articulationTestExam, int number, ArticulationTest articulationTest)
        {
            ArticulationTestExam = articulationTestExam;
            Number = number;
            ArticulationTest = articulationTest;
        }
    }
}
