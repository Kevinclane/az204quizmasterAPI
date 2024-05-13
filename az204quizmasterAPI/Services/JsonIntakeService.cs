using az204quizmasterAPI.Models.RequestModels;
using System.Text.Json;

namespace az204quizmasterAPI.Services
{
    public class JsonIntakeService
    {
        private DataContext _context;
        public JsonIntakeService(DataContext context)
        {
            _context = context;
        }

        public string? IngestJson(JsonIntake jsonIntake)
        {
            if (jsonIntake == null)
            {
                return "error parsing Json";
            }

            string? error = ValidateJson(jsonIntake);

            return error;
        }

        private string? ValidateJson(JsonIntake jsonIntake)
        {
            if (jsonIntake.OptionIntakes.Count < 2)
            {
                return "Question [" + jsonIntake.Question + "] must have at least 2 options.";
            }

            string? error;

            switch (jsonIntake.QuestionType)
            {
                case "MultipleChoiceSingle":
                    error = ValidateMultipleChoiceSingle(jsonIntake);
                    break;
                case "MultipleChoiceMultiple":
                    error = ValidateMultipleChoiceMulitple(jsonIntake);
                    break;
                case "Match":
                    error = ValidateMatch(jsonIntake);
                    break;
                default:
                    return "Invalid QuestionType";
            }

            return error;
        }

        private string? ValidateMultipleChoiceSingle(JsonIntake jsonIntake)
        {
            int correctAnswerCount = GetCorrectAnswerCount(jsonIntake.OptionIntakes);
            if (correctAnswerCount == 0)
            {
                return "Question [" + jsonIntake.Question + "] is missing a correct answer.";
            }
            else if (correctAnswerCount > 1)
            {
                return "Question [" + jsonIntake.Question + "] can only have 1 correct answer.";
            }
            else
            {
                return null;
            }
        }

        private string? ValidateMultipleChoiceMulitple(JsonIntake jsonIntake)
        {
            int correctAnswerCount = GetCorrectAnswerCount(jsonIntake.OptionIntakes);
            if (correctAnswerCount == 0)
            {
                return "Question [" + jsonIntake.Question + "] must have at least one answer.";
            }
            return null;
        }

        private string? ValidateMatch(JsonIntake jsonIntake)
        {
            bool hasValidAnswers = true;
            foreach (OptionIntake option in jsonIntake.OptionIntakes)
            {
                if (option.LeftDisplay == null || option.RightDisplay == null)
                {
                    hasValidAnswers = false;
                }
            }
            return hasValidAnswers ? null : "Question [" + jsonIntake.Question + "] has one or more missing display options.";
        }

        private int GetCorrectAnswerCount(ICollection<OptionIntake> optionIntakes)
        {
            int count = 0;
            foreach (OptionIntake optionIntake in optionIntakes)
            {
                if (optionIntake.IsCorrect == true)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
