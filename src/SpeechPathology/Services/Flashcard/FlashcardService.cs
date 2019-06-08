using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Flashcard
{
    public class FlashcardService : IFlashcardService
    {
        private IRepository<Models.Flashcard> _repositoryFlashcard;
        public FlashcardService(IRepository<Models.Flashcard> repositoryFlashcard)
        {
            _repositoryFlashcard = repositoryFlashcard;
        }

        public async Task<List<string>> GetSounds(string languageCode)
        {
            //var flashcards = await _repositoryFlashcard.AsQueryable().OrderBy(x => x.Sound).ToListAsync();
            var flashcards = await _repositoryFlashcard.GetAsync(predicate: x => x.LanguageCode == languageCode, orderBy: x => x.Sound); 

            var sounds = flashcards.GroupBy(x => new { x.Sound }).Select(x => x.FirstOrDefault()).Select(x => x.Sound).ToList<string>();

            return sounds;
        }

        public async Task<List<string>> GetSoundPositions(string sound, string excludedSound, string languageCode)
        {
            // Order by sound position enum
            var order = Enum.GetValues(typeof(SoundPosition))
                .OfType<SoundPosition>()
                .Select(x => new {
                    Name = x.ToString(),
                    Value = (int)x
            });

            // Get flashcards
            var flashcards = await _repositoryFlashcard.GetAsync<String>(predicate: x => x.Sound == sound
                && x.LanguageCode == languageCode
                && (excludedSound == "" || !x.Text.Contains(excludedSound)));

            // Group & Order
            var soundPositions = flashcards
                .Join(order,
                          f => f.SoundPosition,
                          o => o.Name,
                          (f, o) => new { f.SoundPosition, o.Value })
                .OrderBy(r => r.Value)
                .GroupBy(x => new { x.SoundPosition })
                .Select(x => x.FirstOrDefault())
                .Select(x => x.SoundPosition)
                .ToList<string>();

            return soundPositions;
        }

        public async Task<List<Models.Flashcard>> GetFlashcards(FlashcardSoundPosition soundPosition, string sound, string excludedSound, string languageCode)
        {
            string soundPositionName = Enum.GetName(typeof(FlashcardSoundPosition), soundPosition);

            var flashcards = await _repositoryFlashcard.GetAsync<Models.Flashcard>(predicate: x => x.Sound == sound 
                && x.SoundPosition == soundPositionName 
                && x.LanguageCode == languageCode
                && (excludedSound == "" || !x.Text.Contains(excludedSound)));

            return flashcards;
        }
    }
}
