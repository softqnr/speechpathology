using SpeechPathology.Data;
using SpeechPathology.Models;
using System.Collections.Generic;
using System.Text;

namespace SpeechPathology.DataServices.Flashcard
{

    public class FlashcardService : IFlashcardService
    {
        private IRepository<Models.Flashcard> _repositoryFlashcard;
        public FlashcardService(IRepository<Models.Flashcard> repositoryFlashcard)
        {
            _repositoryFlashcard = repositoryFlashcard;
        }

        public List<string> GetSounds()
        {
            return new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "L" };
        }

        public List<Models.Flashcard> GetFlashcards(string sound, string excludedSound)
        {
            throw new System.NotImplementedException();
        }
    }
}
