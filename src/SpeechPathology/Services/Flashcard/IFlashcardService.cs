using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Flashcard
{
    public interface IFlashcardService
    {
        Task<List<string>> GetSounds();

        Task<List<Models.Flashcard>> GetFlashcards(SoundPosition soundPosition, string sound, string excludedSound);

        Task<List<string>> GetSoundPositions(string soundPosition, string excludedSound);
    }
}
