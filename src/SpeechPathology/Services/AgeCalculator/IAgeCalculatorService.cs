using SpeechPathology.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.ViewModels
{
    public interface IAgeCalculatorService
    {
        Task<List<AgeCalculation>> GetAllAsync();
    }
}