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
            List<Models.Flashcard> flashcards = new List<Models.Flashcard>
                       {
                            new Models.Flashcard() { Text = "Cat", Sound = "books.mp3", Imagefile = "cat.jpg" },
                            new Models.Flashcard() { Text = "Tree", Sound = "bike.mp3", Imagefile = "tree.jpg" },
                            new Models.Flashcard() { Text = "Brushes", Sound = "brushes.mp3", Imagefile = "cherries.jpg" },
                        };

            return flashcards;
        }
    }
}
