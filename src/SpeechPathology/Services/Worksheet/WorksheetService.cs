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
            //var worksheets = await _repositoryWorksheet.GetAllAsync();
            List<Models.Worksheet> worksheets = new List<Models.Worksheet>()
            {
                new Models.Worksheet(){Sound="A", File="a.pdf"},
                new Models.Worksheet(){Sound="B", File="Worksheet_TR_Z.pdf"},
                new Models.Worksheet(){Sound="C", File="a.pdf"},
                new Models.Worksheet(){Sound="D", File="b.pdf"},
                new Models.Worksheet(){Sound="E", File="a.pdf"},
                new Models.Worksheet(){Sound="F", File="b.pdf"},
                new Models.Worksheet(){Sound="G", File="a.pdf"},
                new Models.Worksheet(){Sound="H", File="b.pdf"},
            };
            return worksheets;
        }
    }
}
