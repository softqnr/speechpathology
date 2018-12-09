using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;

namespace SpeechPathology.DataServices.Flashcard
{
    public interface IFlashcardService
    {
        List<string> GetSounds();

        List<Models.Flashcard> GetFlashcards(SoundPosition position, string sound, string excludedSound);
    }
}
