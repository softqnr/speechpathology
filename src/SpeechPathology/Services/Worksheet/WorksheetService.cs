using SpeechPathology.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Worksheet
{
    public class WorksheetService : IWorksheetService
    {
        private readonly IRepository<Models.Worksheet> _repositoryWorksheet;

        public WorksheetService(IRepository<Models.Worksheet> repositoryWorksheet)
        {
            _repositoryWorksheet = repositoryWorksheet;
        }

        public async Task<List<Models.Worksheet>> GetAllAsync(string languageCode)
        {
            var worksheets = await _repositoryWorksheet.GetAsync(predicate: x => x.LanguageCode == languageCode, orderBy: x => x.Sound);

            return worksheets;
        }
    }
}
