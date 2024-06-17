using az204quizmasterAPI.Models.Enums;

namespace az204quizmasterAPI.Models.Entities
{
    public class ActiveQA
    {
        public int Id { get; set; }
        public ActiveQAState State { get; set; } = ActiveQAState.Pending;

        public int QAId { get; set; }
        public QA QA { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
