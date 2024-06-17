using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;

namespace az204quizmasterAPI.Models.ViewModels
{
    public class ActiveQAVM
    {
        public int QuizId { get; set; }
        public int QAId { get; set; }
        public string Question { get; set; }        
        public CategoryEnum Category { get; set; }
        public List<OptionVM> Options { get; set; } = new List<OptionVM>();

        public ActiveQAVM(ActiveQA activeQA)
        {
            QuizId = activeQA.QuizId;
            QAId = activeQA.QAId;
            Question = activeQA.QA.Question;

            foreach (var option in activeQA.QA.Options)
            {
                Options.Add(new OptionVM(option));
            }
        }
    }
}
