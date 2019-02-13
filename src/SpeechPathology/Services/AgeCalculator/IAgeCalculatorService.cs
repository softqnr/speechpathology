using SpeechPathology.Models;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public interface IAgeCalculatorService
    {
        //Task<AgeCalculation> BirthDate(DateTime birthDate);
        //Task<AgeCalculation> TestDate(DateTime testDate);
        //Task<GetTodaysDate> Today(DateTime today);
        Task GetCurrentAge();
    }

    public class GetTodaysDate
    {
    }
}
