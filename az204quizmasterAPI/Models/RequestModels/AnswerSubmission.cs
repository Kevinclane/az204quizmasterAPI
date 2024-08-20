namespace az204quizmasterAPI.Models.RequestModels
{
    public class AnswerSubmission
    {
        public int quizId { get; set; }
        public int aqaId { get; set; }
        public List<int> optionIds { get; set; } = new List<int>();

    }
}
