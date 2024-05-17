using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;
using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class JsonIntake
    {
        public required string Question { get; set; }
        public required QuestionTypeEnum QuestionType { get; set; }
        public string[] ResourceLinks { get; set; }
        public string? Image { get; set; }
        public required CategoryEnum Category { get; set; }
        public ICollection<OptionIntake> Options { get; set; }

        [SetsRequiredMembers]
        public JsonIntake(
        string question,
        QuestionTypeEnum questionType,
        string[] resourceLinks,
        string? image,
        CategoryEnum category,
        ICollection<OptionIntake> options)
        {
            Question = question;
            QuestionType = questionType;
            ResourceLinks = resourceLinks;
            Image = image;
            Category = category;
            Options = options;
        }
    }
}
