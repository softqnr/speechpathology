using SpeechPathology.Models;
using SpeechPathology.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Infrastructure.PDF
{
    public interface IPDFGeneratorService
    {
        string GeneratePDFForPositionTestResults(ArticulationTestExam articulationTestExam);

        Task<string> GeneratePDFForPositionTestResultsAsync(ArticulationTestExam articulationTestExam);

        string GeneratePDFForSoundTestResults(ArticulationTestExam articulationTestExamg, IEnumerable<Grouping<string, ArticulationTestExamAnswer>> articulationTestAnswersGrouping);

        Task<string> GeneratePDFForSoundTestResultsAsync(ArticulationTestExam articulationTestExam, IEnumerable<Grouping<string, ArticulationTestExamAnswer>> articulationTestAnswersGrouping);
    }
}
