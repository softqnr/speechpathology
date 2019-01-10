using SpeechPathology.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Infrastructure.PDF
{
    public interface IPDFGeneratorService
    {
        string GeneratePDF(ArticulationTestExam articulationTestExam);
        Task<string> GeneratePDFAsync(ArticulationTestExam articulationTestExam);
    }
}
