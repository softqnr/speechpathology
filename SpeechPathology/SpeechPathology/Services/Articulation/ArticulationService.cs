using SpeechPathology.Data;
using SpeechPathology.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public class ArticulationService : IArticulationService
    {
        private IRepository<ArticulationTest> repositoryTest;
        private IRepository<ArticulationTestAnswer> repositoryAnswer;

        public ArticulationService()
        {
            var db = new SQLite.SQLiteAsyncConnection(App.DatabasePath);
            repositoryTest = new Repository<ArticulationTest>(db);
            repositoryAnswer = new Repository<ArticulationTestAnswer>(db);
        }

        public async Task<IList<ArticulationTestAnswer>> GenerateTest(string location)
        {
            IList<ArticulationTestAnswer> answers = await repositoryAnswer.GetAll();
            // Delete previous answers if any
            await repositoryAnswer.DeleteAllAsync(answers);
            // Select tests based on sound position
            var tests = await repositoryTest.Get(predicate: x => x.Position == location, orderBy: x => x.Sound);
            foreach (ArticulationTest test in tests)
            {
                await repositoryAnswer.Insert(new ArticulationTestAnswer(tests.IndexOf(test) + 1, test.Id));
            }
            return await repositoryAnswer.GetAllWithChildren();
        }

        public async Task<IList<ArticulationTestAnswer>> GetPendingTest()
        {
            return await repositoryAnswer.GetAllWithChildren(/*predicate: x => x.IsCorrect == null*/);
        }

        public async Task<int> Answer(ArticulationTestAnswer articulationTestAnwser, bool isCorrect)
        {
            articulationTestAnwser.IsCorrect = isCorrect;
            return await repositoryAnswer.Update(articulationTestAnwser);
        }

        public async Task<bool> PendingTestExists()
        {
            var unanswered = await repositoryAnswer.Get(predicate: x => x.IsCorrect == null, orderBy: x => x.Id);
            return unanswered.Count > 0;
        }




    }
}
