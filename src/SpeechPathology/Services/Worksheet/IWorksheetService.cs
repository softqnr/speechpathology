using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Worksheet
{
    public interface IWorksheetService
    {
        Task<List<Models.Worksheet>> GetAllAsync(string languageCode);
    }
}
