using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class JsonIntake
    {
        [SetsRequiredMembers]
        public JsonIntake(
            string question,
            string questionType,
            string? resourceLink,
            string? referenceLink,
            string? image,
            ICollection<OptionIntake> optionIntakes
            )
        {
            Question = question;
            QuestionType = questionType;
            ResourceLink = resourceLink;
            ReferenceLink = referenceLink;
            Image = image;
            OptionIntakes = optionIntakes;
        }
        public required string Question { get; set; }
        public required string QuestionType { get; set; }
        public string? ResourceLink { get; set; }
        public string? ReferenceLink { get; set; }
        public string? Image { get; set; }
        public ICollection<OptionIntake> OptionIntakes { get; set; }
    }
}
