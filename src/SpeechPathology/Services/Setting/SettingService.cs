using SpeechPathology.Data;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Setting
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Models.Setting> _repositorySetting;

        public SettingService(IRepository<Models.Setting> repositorySetting)
        {
            _repositorySetting = repositorySetting;
        }

        public async Task<string> GetByName(string name)
        {
            var setting = await _repositorySetting.AsQueryable().Where(x => x.Name == name).FirstOrDefaultAsync();

            return setting.Value;
        }
    }
}
