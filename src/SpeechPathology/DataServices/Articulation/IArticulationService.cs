using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.DataServices.Articulation
{
    public interface IArticulationService
    {
        Task<IList<ArticulationTestAnswer>> GenerateTest(SoundPosition soundPosition);
        Task<int> Answer(ArticulationTestAnswer articulationTest, bool isCorrect);
        Task<bool> PendingTestExists();
    }
}
