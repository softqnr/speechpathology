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

        public async Task<List<string>> GetSounds()
        {
            var flashcards = await _repositoryFlashcard.AsQueryable().OrderBy(x => x.Sound).ToListAsync();

            var sounds = flashcards.GroupBy(x => new { x.Sound }).Select(x => x.FirstOrDefault()).Select(x => x.Sound).ToList<string>();

            return sounds;
        }

        public async Task<List<string>> GetSoundPositions(string sound, string excludedSound)
        {
            var flashcards = await _repositoryFlashcard.GetAsync<String>(predicate: x => x.Sound == sound
                && (excludedSound == "" || !x.Text.Contains(excludedSound)));

            var soundPositions = flashcards.GroupBy(x => new { x.SoundPosition }).Select(x => x.FirstOrDefault()).Select(x => x.SoundPosition).ToList<string>();
            
            // Convert to Enum
            //var soundPositionsEnum =  soundPositions.Select(s => (FlashcardSoundPosition)Enum.Parse(typeof(FlashcardSoundPosition), s, true)).ToList();

            return soundPositions;
        }

        public async Task<List<Models.Flashcard>> GetFlashcards(FlashcardSoundPosition soundPosition, string sound, string excludedSound)
        {
            string soundPositionName = Enum.GetName(typeof(FlashcardSoundPosition), soundPosition);

            var flashcards = await _repositoryFlashcard.GetAsync<Models.Flashcard>(predicate: x => x.Sound == sound 
                && x.SoundPosition == soundPositionName 
                && (excludedSound == "" || !x.Text.Contains(excludedSound)));

            return flashcards;
        }
    }
}
