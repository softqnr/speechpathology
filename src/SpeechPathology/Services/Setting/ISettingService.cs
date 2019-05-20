using System.Threading.Tasks;

namespace SpeechPathology.Services.Setting
{
    public interface ISettingService
    {
        Task<string> GetByName(string name);
    }
}
