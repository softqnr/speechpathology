using SpeechPathology.Data;
using SpeechPathology.Models;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public class AgeCalculatorService : IAgeCalculatorService
    {
        private IRepository<AgeCalculation> _ageCalculation;

        public AgeCalculatorService(IRepository<AgeCalculation> ageCalculation)
        {
            _ageCalculation = ageCalculation;
        }

        public async Task<AgeCalculation> BirthDate(DateTime birthDate)
        {
            throw new NotImplementedException();
        }

        public async Task<AgeCalculation> TestDate(DateTime testDate)
        {
            throw new NotImplementedException();
        }

        public async Task<GetTodaysDate> Today(DateTime today)
        {
            throw new NotImplementedException();
        }

        public Task GetCurrentAge()
        {
            throw new NotImplementedException();
        }
    }
}
