using az204quizmasterAPI.Models.Enums;
using az204quizmasterAPI.Models.RequestModels;
using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.Entities
{
    public class QA
    {
        public int Id { get; set; }
        public required string Question { get; set; }
        public required QuestionTypeEnum QuestionType { get; set; }
        public string? ResourceLink { get; set; }
        public string? Image { get; set; }
        public CategoryEnum Category { get; set; }
        public ICollection<Option> Options { get; set; } = null!;


        [SetsRequiredMembers]
        public QA(string question, QuestionTypeEnum questionType, string? resourceLink, string? image, CategoryEnum category)
        {
            Question = question;
            QuestionType = questionType;
            ResourceLink = resourceLink;
            Image = image;
            Category = category;
        }
    }
}
