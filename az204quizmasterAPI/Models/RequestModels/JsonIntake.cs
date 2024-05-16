using az204quizmasterAPI.Models.Enums;
using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class JsonIntake
    {
        [SetsRequiredMembers]
        public JsonIntake(
            string question,
            QuestionTypeEnum questionType,
            string? resourceLink,
            string? image,
            CategoryEnum category,
            ICollection<OptionIntake> optionIntakes
            )
        {
            Question = question;
            QuestionType = questionType;
            ResourceLink = resourceLink;
            Image = image;
            Category = category;
            OptionIntakes = optionIntakes;
        }
        public required string Question { get; set; }
        public required QuestionTypeEnum QuestionType { get; set; }
        public string? ResourceLink { get; set; }
        public string? Image { get; set; }
        public required CategoryEnum Category { get; set; }
        public ICollection<OptionIntake> OptionIntakes { get; set; }
    }
}
