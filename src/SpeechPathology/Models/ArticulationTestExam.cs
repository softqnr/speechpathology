using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace SpeechPathology.Models
{
    [Table("ArticulationTestExams")]
    public class ArticulationTestExam : ModelBase
    {
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        public string SoundPosition { get; set; }
        public int AgeY { get; set; }
        public int AgeM { get; set; }
        public double? Score { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ArticulationTestExamAnswer> Answers { get; set; }

        public ArticulationTestExam() { }

        public ArticulationTestExam(string soundPosition) {
            DateStarted = DateTime.Now;
            SoundPosition = soundPosition;
        }

        public ArticulationTestExam(Tuple<int,int> age)
        {
            DateStarted = DateTime.Now;
            SoundPosition = "";
            AgeY = age.Item1;
            AgeM = age.Item2;
        }
    }
}
