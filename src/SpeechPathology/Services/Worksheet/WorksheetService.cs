using SpeechPathology.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Worksheet
{
    public class WorksheetService : IWorksheetService
    {
        private IRepository<Models.Worksheet> _repositoryWorksheet;
        public WorksheetService(IRepository<Models.Worksheet> repositoryWorksheet)
        {
            _repositoryWorksheet = repositoryWorksheet;
        }
        public async Task<List<Models.Worksheet>> GetAllAsync()
        {
            var worksheets = await _repositoryWorksheet.GetAllAsync();

            return worksheets;
        }
    }
}
