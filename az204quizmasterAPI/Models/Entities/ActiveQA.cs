using az204quizmasterAPI.Models.Enums;

namespace az204quizmasterAPI.Models.Entities
{
    public class ActiveQA
    {
        public int Id { get; set; }
        public List<int> SubmittedAnswers { get; set; } = new List<int>();

        public int QAId { get; set; }
        public QA QA { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
