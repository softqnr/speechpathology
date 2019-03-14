using SpeechPathology.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public interface IAgeCalculatorService
    {
        Task<List<AgeCalculation>> GetAllAsync();
    }
}