using SpeechPathology.Models;
using System.Collections.Generic;

namespace SpeechPathology.DataServices.Flashcard
{
    public interface IFlashcardService
    {
        List<string> GetSounds();

        List<Models.Flashcard> GetFlashcards(string sound, string excludedSound);
    }
}
