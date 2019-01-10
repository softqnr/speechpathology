using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SpeechPathology.Services.Articulation
{
    public class ArticulationTestService : IArticulationTestService
    {
        private IRepository<ArticulationTest> _repositoryTest;
        private IRepository<ArticulationTestExam> _repositoryTestExam;
        private IRepository<ArticulationTestExamAnswer> _repositoryTestExamAnswer;

        public ArticulationTestService(IRepository<ArticulationTest> repositoryTest,
            IRepository<ArticulationTestExam> repositoryExam,
            IRepository<ArticulationTestExamAnswer> repositoryExamAnswer)
        {
            _repositoryTest = repositoryTest;
            _repositoryTestExam = repositoryExam;
            _repositoryTestExamAnswer = repositoryExamAnswer;
        }

        public async Task<ArticulationTestExam> GenerateExam(SoundPosition soundPosition)
        {
            // Delete previous exams
            await DeleteAllExams();

            // Get position name from enum
            string soundPositionName = Enum.GetName(typeof(SoundPosition), soundPosition);
            // Get tests by sound position
            var tests = await _repositoryTest.GetAsync(predicate: x => x.SoundPosition == soundPositionName,
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

        public async Task<int> Answer(ArticulationTestExamAnswer articulationTestAnwser, bool isCorrect)
        {
            articulationTestAnwser.IsCorrect = isCorrect;
            return await _repositoryTestExamAnswer.UpdateAsync(articulationTestAnwser);
        }

        public async Task<ArticulationTestExam> CloseExam(ArticulationTestExam exam){
            exam.DateEnded = DateTime.Now;
            // Calculate score
            exam.Score = exam.Answers.DefaultIfEmpty().Average(x => Convert.ToInt32(x.IsCorrect));
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
    }
}
