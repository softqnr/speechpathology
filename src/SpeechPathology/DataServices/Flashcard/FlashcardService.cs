using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
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
            return new List<string> { "m",
                "n",
                "h",
                "p",
                "b",
                "t",
                "k",
                "w",
                "d",
                "g",
                "f",
                "ng",
                "l",
                "s",
                "v",
                "y",
                "ch",
                "r",
                "j",
                "sh",
                "th",
                "zh",
            };
        }

        public List<Models.Flashcard> GetFlashcards(SoundPosition position, string sound, string excludedSound)
        {
            throw new System.NotImplementedException();
        }
    }
}
