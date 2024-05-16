using az204quizmasterAPI.Models.RequestModels;
using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public required string LeftDisplay { get; set; }
        public string? RightDisplay { get; set; }
        public bool? IsCorrect { get; set; }

        public int QAId { get; set; }
        public QA QA { get; set; } = null!;

        [SetsRequiredMembers]
        public Option(string leftDisplay, string? rightDisplay, bool? isCorrect)
        {
            LeftDisplay = leftDisplay;
            RightDisplay = rightDisplay;
            IsCorrect = isCorrect;
        }
    }
}
