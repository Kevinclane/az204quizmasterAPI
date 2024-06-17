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

        //[HttpGet]
        //public ActionResult 

        // POST: QuizController/Create
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


        [HttpGet]
        [Route("nextQuestion/{id}")]
        public ActiveQAVM? GetNextQuestion(int id)
        {
            return _quizService.GetNextQuestion(id);
        }

        //[HttpPost]
        //[Route("submitAnswer")]

    }
}
