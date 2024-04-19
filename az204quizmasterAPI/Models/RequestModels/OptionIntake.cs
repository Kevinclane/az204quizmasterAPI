using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class OptionIntake
    {
        [SetsRequiredMembers]
        public OptionIntake(string leftDisplay, string? rightDisplay, bool? isCorrect)
        {
            LeftDisplay = leftDisplay;
            RightDisplay = rightDisplay;
            IsCorrect = isCorrect;
        }
        public required string LeftDisplay { get; set; }
        public string? RightDisplay { get; set; }
        public bool? IsCorrect { get; set; }

    }
}
