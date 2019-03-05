using SpeechPathology.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public interface IAgeCalculatorService
    {
        //Task<AgeCalculation> BirthDate(DateTime birthDate);
        //Task<AgeCalculation> TestDate(DateTime testDate);
        //Task<GetTodaysDate> Today(DateTime today);
        Task GetCurrentAge();
        Task<List<AgeCalculation>> GetAllAsync();
    }

    public class GetTodaysDate
    {
    }
}
