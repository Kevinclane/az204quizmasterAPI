using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;
using az204quizmasterAPI.Models.RequestModels;
using az204quizmasterAPI.Models.ViewModels;

namespace az204quizmasterAPI.Services
{
    public class QuizService
    {
        private DataContext _context;

        public QuizService(DataContext context)
        {
            _context = context;
        }

        private string GenerateClause(int clause)
        {
            return "Category = " + clause;
        }

        public int CreateLimitedQuiz(QuizRequest quizRequest)
        {
            var quiz = new Quiz();

            int categoryQuestionCount = quizRequest.QuestionCount / quizRequest.getCategories().Count;
            int remainder = quizRequest.QuestionCount % quizRequest.getCategories().Count;

            List<QA> qas = new List<QA>();

            foreach (int category in quizRequest.getCategories())
            {
                int queryCount = categoryQuestionCount;
                if (remainder > 0)
                {
                    queryCount++;
                    remainder--;
                }

                var categoryGroupedQas = _context.QAs.FromSqlRaw($"SELECT * FROM qas WHERE {GenerateClause(category)} LIMIT {queryCount}").ToList();
                qas.AddRange(categoryGroupedQas);
            }

            foreach (QA qa in qas)
            {
                quiz.ActiveQAs.Add(new ActiveQA { QA = qa, QAId = qa.Id });
            }

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            return quiz.Id;
        }

        public int CreateFullQuiz(QuizRequest quizRequest)
        {
            var quiz = new Quiz();

            List<int> clauses = quizRequest.getCategories();
            String clausesJoined = String.Join(" OR ", clauses.Select(clause => GenerateClause(clause)));

            var qas = _context.QAs.FromSqlRaw($"Select * FROM qas WHERE {clausesJoined}").ToList();

            foreach (QA qa in qas)
            {
                quiz.ActiveQAs.Add(new ActiveQA { QA = qa, QAId = qa.Id });
            }

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            return quiz.Id;
        }

        public int CreateQuiz(QuizRequest quizRequest)
        {
            if (!quizRequest.isValid())
            {
                return -1;
            }

            if (quizRequest.QuestionCount <= 0)
            {
                return CreateFullQuiz(quizRequest);
            } 
            else
            {
                return CreateLimitedQuiz(quizRequest);
            }
        }

        public void SubmitAnswer(AnswerSubmission answerSubmission)
        {
            if (answerSubmission.optionIds.Count != 0 && answerSubmission.aqaId != 0)
            {
                _context.ActiveQAs
                    .Where(aqa => aqa.Id == answerSubmission.aqaId)
                    .ExecuteUpdate(setters => setters.SetProperty(a => a.SubmittedAnswers, answerSubmission.optionIds));
            }
        }

        public ActiveQAVM? GetNextQuestion(AnswerSubmission answerSubmission)
        {
            List<int> ids = _context.ActiveQAs
                .Where(aqa => aqa.QuizId == answerSubmission.quizId && aqa.SubmittedAnswers == new List<int>())
                .Select(aqa => aqa.Id)
                .ToList();

            if(ids.Count == 0)
            {
                return null;
            }

            int randomIndex = new Random().Next(0, ids.Count);
            
            var ActiveQA = _context.ActiveQAs
                .Include(aqa => aqa.QA)
                .ThenInclude(qa => qa.Options)
                .FirstOrDefault(aqa => aqa.Id == ids[randomIndex]);

            if(ActiveQA == null)
            {
                return null;
            }

            var ActiveQAVM = new ActiveQAVM(ActiveQA);
            ActiveQAVM.TotalQuestionCount = _context.ActiveQAs.Count(aqa => aqa.QuizId == answerSubmission.quizId);
            ActiveQAVM.FinishedQuestionCount = _context.ActiveQAs.Count(aqa => aqa.QuizId == answerSubmission.quizId && aqa.SubmittedAnswers != new List<int>());

            return ActiveQAVM;
        }

        public Quiz? GetResults(int quizId)
        {
            var quiz = _context.Quizzes
                .Include(q => q.ActiveQAs)
                .ThenInclude(aqa => aqa.QA)
                .ThenInclude(qa => qa.Options)
                .FirstOrDefault(q => q.Id == quizId);

            return quiz;
        }
    }
}
