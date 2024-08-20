using az204quizmasterAPI.Models.RequestModels;
using az204quizmasterAPI.Models.ViewModels;
using az204quizmasterAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace az204quizmasterAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;
        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        [Route("create")]
        public int CreateQuiz(QuizRequest quizRequest)
        {
            try
            {
                return _quizService.CreateQuiz(quizRequest);
            }
            catch
            {
                return -1;
            }
        }


        [HttpPost]
        [Route("submitOrNext")]
        public ActiveQAVM? SubmitAndNextQuestion(AnswerSubmission answerSubmission)
        {
            _quizService.SubmitAnswer(answerSubmission);
            return _quizService.GetNextQuestion(answerSubmission);
        }

    }
}
