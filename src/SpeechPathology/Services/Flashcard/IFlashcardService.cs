using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Flashcard
{
    public interface IFlashcardService
    {
        Task<List<string>> GetSounds(string languageCode);

        Task<List<Models.Flashcard>> GetFlashcards(FlashcardSoundPosition soundPosition, string sound, string excludedSound, string languageCode);

        Task<List<string>> GetSoundPositions(string soundPosition, string excludedSound, string languageCode);
    }
}
