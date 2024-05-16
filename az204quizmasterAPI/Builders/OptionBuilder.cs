using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.RequestModels;

namespace az204quizmasterAPI.Builders
{
    public class OptionBuilder
    {
        public static Option BuildOption(OptionIntake optionIntake, QA qa)
        {
            Option option = new Option(
                optionIntake.LeftDisplay,
                optionIntake.RightDisplay,
                optionIntake.IsCorrect
            );

            option.QA = qa;
            return option;
        }
    }
}
