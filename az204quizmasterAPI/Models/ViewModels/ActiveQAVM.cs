using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;

namespace az204quizmasterAPI.Models.ViewModels
{
    public class ActiveQAVM
    {
        public int QuizId { get; set; }
        public int QAId { get; set; }
        public int AQAid { get; set; }
        public int TotalQuestionCount { get; set; }
        public int FinishedQuestionCount { get; set; }
        public string Question { get; set; }
        public string Image { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public CategoryEnum Category { get; set; }
        public List<OptionVM> Options { get; set; } = new List<OptionVM>();
        public ICollection<ResourceLink> Links { get; set; } = new List<ResourceLink>();

        public ActiveQAVM(ActiveQA activeQA)
        {
            QuizId = activeQA.QuizId;
            QAId = activeQA.QAId;
            AQAid = activeQA.Id;
            Question = activeQA.QA.Question;
            Image = activeQA.QA.Image;
            QuestionType = activeQA.QA.QuestionType;
            Category = activeQA.QA.Category;
            Links = activeQA.QA.Links;

            foreach (var option in activeQA.QA.Options)
            {
                Options.Add(new OptionVM(option));
            }
        }
    }
}
