using SpeechPathology.Data;
using SpeechPathology.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<AgeCalculation>> GetAllAsync()
        {
            var docs = await _ageCalculation.GetAllAsync();
            return docs;
        }

        public async Task GetCurrentAge()
        {
            throw new NotImplementedException();
        }
    }
}
