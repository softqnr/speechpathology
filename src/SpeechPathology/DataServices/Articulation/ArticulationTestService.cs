using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SpeechPathology.DataServices.Articulation
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
        // TODO: Investigate InsertOrReplaceWithChildrenAsync malfunction
        public async Task<ArticulationTestExam> GenerateExam(SoundPosition soundPosition)
        {
            // Get position name from enum
            string soundPositionName = Enum.GetName(typeof(SoundPosition), soundPosition);
            // Get tests by sound position
            var tests = await _repositoryTest.GetAsync(predicate: x => x.SoundPosition == soundPositionName,
                orderBy: x => x.Sound);

            // Delete previous exams
            List<ArticulationTestExam> exams = await _repositoryTestExam.GetAllWithChildrenAsync();
            if (exams.Count > 0)
            {               
                await _repositoryTestExam.DeleteAllAsync(exams, true);
            }

            // Create new exam
            var exam = new ArticulationTestExam(soundPositionName);
            int examId = await _repositoryTestExam.InsertAsync(exam);
            // Create exam answers
            int indexNumber = 0;
            var answers = tests.Select(test =>
            {
                indexNumber += 1;
                return new ArticulationTestExamAnswer(exam, indexNumber, test);
            }).ToList();
            await _repositoryTestExamAnswer.InsertAllWithChildrenAsync(answers, true);
            
            // Get exam with children
            return await _repositoryTestExam.GetWithChildrenAsync(exam.Id, true); ;
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
    }
}
