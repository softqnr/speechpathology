using SpeechPathology.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public interface IArticulationService
    {
        Task<IList<ArticulationTestAnswer>> GenerateTest(string location);
        Task<int> Answer(ArticulationTestAnswer articulationTest, bool isCorrect);
        Task<bool> PendingTestExists();
    }
}
