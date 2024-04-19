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

        public string? ingestJson(string jsonString)
        {
            JsonIntake? jsonIntake = JsonSerializer.Deserialize<JsonIntake>(jsonString);
            if (jsonIntake == null)
            {
                return "error parsing Json";
            }

            string? error = validateJson(jsonIntake);

            return error;
        }

        private string? validateJson(JsonIntake jsonIntake)
        {
            if (jsonIntake.OptionIntakes.Count < 2)
            {
                return jsonIntake.Question + ": must have at least 2 options.";
            }

            string? error;

            switch (jsonIntake.QuestionType)
            {
                case "MultipleChoiceSingle":
                    error = validateMultipleChoiceSingle(jsonIntake);
                    break;
                case "MultipleChoiceMultiple":
                    error = validateMultipleChoiceMulitple(jsonIntake);
                    break;
                case "Match":
                    error = validateMatch(jsonIntake);
                    break;
                default:
                    return "Invalid QuestionType";
            }

            return error;
        }

        private string? validateMultipleChoiceSingle(JsonIntake jsonIntake)
        {
            int correctAnswerCount = getCorrectAnswerCount(jsonIntake.OptionIntakes);
            if (correctAnswerCount == 0)
            {
                return "Question[ " + jsonIntake.Question + "] is missing a correct answer.";
            }
            else if (correctAnswerCount > 1)
            {
                return "Question[ " + jsonIntake.Question + "] can only have 1 correct answer.";
            }
            else
            {
                return null;
            }
        }

        private string? validateMultipleChoiceMulitple(JsonIntake jsonIntake)
        {
            int correctAnswerCount = getCorrectAnswerCount(jsonIntake.OptionIntakes);
            if (correctAnswerCount == 0)
            {
                return "Question[ " + jsonIntake.Question + "] must have at least one answer.";
            }
            return null;
        }

        private string? validateMatch(JsonIntake jsonIntake)
        {
            bool hasValidAnswers = true;
            foreach (OptionIntake option in jsonIntake.OptionIntakes)
            {
                if (option.LeftDisplay == null || option.RightDisplay == null)
                {
                    hasValidAnswers = false;
                }
            }
            return hasValidAnswers ? null : "Question[ " + jsonIntake.Question + "] has one or more missing display options.";
        }

        private int getCorrectAnswerCount(ICollection<OptionIntake> optionIntakes)
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
