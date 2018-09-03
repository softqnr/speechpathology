using SpeechPathology.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.DataServices.Articulation
{
    public interface IArticulationService
    {
        Task<IList<ArticulationTestAnswer>> GenerateTest(string location);
        Task<int> Answer(ArticulationTestAnswer articulationTest, bool isCorrect);
        Task<bool> PendingTestExists();
    }
}
