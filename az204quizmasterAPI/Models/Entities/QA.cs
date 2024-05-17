using az204quizmasterAPI.Models.Enums;
using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.Entities
{
    public class QA
    {
        public int Id { get; set; }
        public required string Question { get; set; }
        public required QuestionTypeEnum QuestionType { get; set; }
        public string? Image { get; set; }

        public ICollection<ResourceLink> Links { get; set; }
        public CategoryEnum Category { get; set; }
        public ICollection<Option> Options { get; set; } = null!;

        [SetsRequiredMembers]
        public QA(string question, QuestionTypeEnum questionType, string? image, CategoryEnum category)
        {
            Question = question;
            QuestionType = questionType;
            Image = image;
            Category = category;
            Links = new List<ResourceLink>();
        }
    }
}
