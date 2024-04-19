using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.Entities
{
    public class QA
    {
        public int Id { get; set; }
        public required string Question { get; set; }
        public required string QuestionType { get; set; }
        public string? ResourceLink { get; set; }
        public string? ReferenceLink { get; set; }
        public string? Image { get; set; }

        public ICollection<Option> Options { get; set; }

        [SetsRequiredMembers]
        public QA(int id, string question, string questionType, string? resourceLink, string? referenceLink, string? image, ICollection<Option> options)
        {
            Id = id;
            Question = question;
            QuestionType = questionType;
            ResourceLink = resourceLink;
            ReferenceLink = referenceLink;
            Image = image;
            Options = options;
        }
    }
}
