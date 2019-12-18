using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using SpeechPathology.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public class ArticulationTestService : IArticulationTestService
    {
        private readonly IRepository<ArticulationTest> _repositoryTest;
        private readonly IRepository<ArticulationTestExam> _repositoryTestExam;
        private readonly IRepository<ArticulationTestExamAnswer> _repositoryTestExamAnswer;

        public ArticulationTestService(IRepository<ArticulationTest> repositoryTest,
            IRepository<ArticulationTestExam> repositoryExam,
            IRepository<ArticulationTestExamAnswer> repositoryExamAnswer)
        {
            _repositoryTest = repositoryTest;
            _repositoryTestExam = repositoryExam;
            _repositoryTestExamAnswer = repositoryExamAnswer;
        }

        public async Task<ArticulationTestExam> GetLastNotFinishedTest()
        {
            ArticulationTestExam exam = null;
            var exams = await _repositoryTestExam.GetAllWithChildrenAsync(predicate: x => x.DateEnded == null);
            // Exam with empty DateEnded means not finished test 
            if (exams.Count > 0){
                exam = exams[0];
            }
            return exam;
        }

        public async Task<ArticulationTestExam> GenerateExam(SoundPosition soundPosition, string languageCode)
        {
            // Delete previous exams
            await DeleteAllExams();

            // Get position name from enum
            string soundPositionName = Enum.GetName(typeof(SoundPosition), soundPosition);
            // Get tests by sound position
            var tests = await _repositoryTest.GetAsync(predicate: x => x.SoundPosition == soundPositionName && x.LanguageCode == languageCode,
                orderBy: x => x.Sound);
            // Create new exam
            var exam = new ArticulationTestExam(soundPositionName);
            // Create exam answers for each test
            int indexNumber = 0;
            exam.Answers = tests.Select(test =>
            {
                indexNumber += 1;
                return new ArticulationTestExamAnswer(indexNumber, test);
            }).ToList();
            // Save exam
            await _repositoryTestExam.InsertWithChildrenAsync(exam, true);

            return exam;
        }

        public async Task<ArticulationTestExam> GenerateExam(Tuple<int,int> age, string languageCode)
        {
            // Delete previous exams
            await DeleteAllExams();
            // Get tests by sound position
            string blended = Enum.GetName(typeof(SoundPosition), SoundPosition.Blended);
            var tests = await _repositoryTest.GetAsync(predicate: x => x.LanguageCode==languageCode && x.SoundPosition!=blended && (x.AgeY<age.Item1 || x.AgeY==age.Item1 && x.AgeM<=age.Item2),
                orderBy: x => x.AgeY);

            tests.OrderBy(x => x.AgeY).ThenBy(x => x.AgeM);
            
            if (tests.Count == 0)
                return null;

            // Create new exam
            var exam = new ArticulationTestExam(age);
            // Create exam answers for each test
            int indexNumber = 0;
            exam.Answers = tests.Select(test =>
            {
                indexNumber += 1;
                return new ArticulationTestExamAnswer(indexNumber, test);
            }).ToList();
            // Save exam
            await _repositoryTestExam.InsertWithChildrenAsync(exam, true);

            return exam;
        }

        public async Task<int> Answer(ArticulationTestExamAnswer articulationTestAnwser, bool isCorrect)
        {
            articulationTestAnwser.IsCorrect = isCorrect;
            return await _repositoryTestExamAnswer.UpdateAsync(articulationTestAnwser);
        }

        public async Task<ArticulationTestExam> CloseExam(ArticulationTestExam exam){
            exam.DateEnded = DateTime.Now;
            // Calculate score
            exam.Score = CalculateScore(exam);
            // Save
            await _repositoryTestExam.UpdateAsync(exam);

            return await _repositoryTestExam.GetWithChildrenAsync(exam.Id, true);
        }

        public async Task DeleteAllExams()
        {
            List<ArticulationTestExam> exams = await _repositoryTestExam.GetAllWithChildrenAsync();
            if (exams.Count > 0)
            {
                await _repositoryTestExam.DeleteAllAsync(exams, true);
            }
        }

        private double CalculateScore(ArticulationTestExam exam)
        {
            return exam.Answers.DefaultIfEmpty()
                .Average(x => Convert.ToInt32(x.IsCorrect));
        }

        public IEnumerable<Grouping<string, ArticulationTestExamAnswer>> GenerateGroupings(ArticulationTestExam exam)
        {
            var result = from answer in exam.Answers
                         orderby answer.ArticulationTest.Sound
                         group answer by answer.ArticulationTest.Sound into answerGroup
                         select new Grouping<string, ArticulationTestExamAnswer>($"{answerGroup.Key}  ({answerGroup.Average(x => Convert.ToInt32(x.IsCorrect)):P})", answerGroup.Key, answerGroup);

            return result;
        }
    }
}
