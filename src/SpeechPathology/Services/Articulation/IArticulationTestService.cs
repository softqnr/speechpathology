using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public interface IArticulationTestService
    {
        Task<ArticulationTestExam> GetLastNotFinishedTest();

        Task<ArticulationTestExam> GenerateExam(SoundPosition soundPosition, string languageCode);

        Task<ArticulationTestExam> GenerateExam(Tuple<int, int> age, string languageCode);

        Task<int> Answer(ArticulationTestExamAnswer articulationTest, bool isCorrect);

        Task<ArticulationTestExam> CloseExam(ArticulationTestExam exam);

        Task DeleteAllExams();

        IEnumerable<Grouping<string, ArticulationTestExamAnswer>> GenerateGroupings(ArticulationTestExam exam);
    }
}
