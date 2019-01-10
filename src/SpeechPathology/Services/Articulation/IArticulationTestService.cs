﻿using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public interface IArticulationTestService
    {
        Task<ArticulationTestExam> GenerateExam(SoundPosition soundPosition);
        Task<int> Answer(ArticulationTestExamAnswer articulationTest, bool isCorrect);
        Task<ArticulationTestExam> CloseExam(ArticulationTestExam exam);
        Task DeleteAllExams();
    }
}
