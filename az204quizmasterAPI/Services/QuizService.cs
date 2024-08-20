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

        private string generateClause(int clause)
        {
            return "Category = " + clause;
        }

        public int CreateQuiz(QuizRequest quizRequest)
        {
            if (quizRequest.isValid())
            {
                var quiz = new Quiz();

                List<int> clauses = new List<int>();

                if (quizRequest.Compute)
                {
                    clauses.Add((int)CategoryEnum.ComputeSolutions);
                }

                if (quizRequest.Storage)
                {
                    clauses.Add((int)CategoryEnum.Storage);
                }

                if (quizRequest.Security)
                {
                    clauses.Add((int)CategoryEnum.Security);
                }

                if (quizRequest.Monitor)
                {
                    clauses.Add((int)CategoryEnum.Monitor);
                }

                if (quizRequest.ThirdParty)
                {
                    clauses.Add((int)CategoryEnum.ThirdParty);
                }
                string clausesJoined = string.Join(" OR ", clauses.Select(clause => generateClause(clause)));

                var qas = _context.QAs.FromSqlRaw($"Select * FROM qas WHERE {clausesJoined}").ToList();

                foreach (QA qa in qas)
                {
                    quiz.ActiveQAs.Add(new ActiveQA { QA = qa, QAId = qa.Id });
                }

                _context.Quizzes.Add(quiz);
                _context.SaveChanges();

                return quiz.Id;
            }

            return -1;
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
            var ActiveQA = _context.ActiveQAs
                .Include(aqa => aqa.QA)
                .ThenInclude(qa => qa.Options)
                .FirstOrDefault(aqa => aqa.QuizId == answerSubmission.quizId && aqa.SubmittedAnswers == new List<int>());

            return ActiveQA != null ? new ActiveQAVM(ActiveQA) : null;                           
        }
    }
}
