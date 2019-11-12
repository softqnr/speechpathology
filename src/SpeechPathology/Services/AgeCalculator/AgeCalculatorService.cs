using SpeechPathology.Data;
using SpeechPathology.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public class AgeCalculatorService : IAgeCalculatorService
    {
        private readonly IRepository<AgeCalculation> _repositoryAgeCalculation;

        public AgeCalculatorService(IRepository<AgeCalculation> repositoryAgeCalculation)
        {
            _repositoryAgeCalculation = repositoryAgeCalculation;
        }

        public async Task<List<AgeCalculation>> GetAllAsync(string languageCode)
        {
            var ac = await _repositoryAgeCalculation.GetAsync(predicate: x => x.LanguageCode == languageCode, orderBy: x => x.AgeInYears);

            ac.OrderBy(x => x.AgeInYears).ThenBy(x => x.Months).ToList();

            return ac;
        }
    }
}
