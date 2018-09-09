using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SpeechPathology.DataServices.Articulation
{
    public class ArticulationService : IArticulationService
    {
        private IRepository<ArticulationTest> _repositoryTest;
        private IRepository<ArticulationTestAnswer> _repositoryAnswer;

        public ArticulationService(IRepository<ArticulationTest> repositoryTest, IRepository<ArticulationTestAnswer> repositoryAnswer)
        {
            _repositoryTest = repositoryTest;
            _repositoryAnswer = repositoryAnswer;
        }

        public async Task<IList<ArticulationTestAnswer>> GenerateTest(SoundPosition soundPosition)
        {
            IList<ArticulationTestAnswer> answers = await _repositoryAnswer.GetAll();
            // Delete previous answers if any
            if (answers.Count > 0)
            {               
                await _repositoryAnswer.DeleteAllAsync(answers);
            }
            // Get position name from enum
            string soundPositionName = Enum.GetName(typeof(SoundPosition), soundPosition).ToUpper();
            // Select tests based on sound position
            var tests = await _repositoryTest.Get(predicate: x => x.Position == soundPositionName, 
                orderBy: x => x.Sound);
            // Create answers
            var testAnswers = tests.Select(t => new ArticulationTestAnswer(tests.IndexOf(t) + 1, t.Id)).ToArray();
            // Bulk insert
            await _repositoryAnswer.InsertAllAsync(testAnswers);
            
            return await _repositoryAnswer.GetAllWithChildren();
        }

        public async Task<IList<ArticulationTestAnswer>> GetPendingTest()
        {
            return await _repositoryAnswer.GetAllWithChildren(/*predicate: x => x.IsCorrect == null*/);
        }

        public async Task<int> Answer(ArticulationTestAnswer articulationTestAnwser, bool isCorrect)
        {
            articulationTestAnwser.IsCorrect = isCorrect;
            return await _repositoryAnswer.Update(articulationTestAnwser);
        }

        public async Task<bool> PendingTestExists()
        {
            var unanswered = await _repositoryAnswer.Get(predicate: x => x.IsCorrect == null, orderBy: x => x.Id);
            return unanswered.Count > 0;
        }




    }
}
