using az204quizmasterAPI.Builders;
using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;
using az204quizmasterAPI.Models.RequestModels;

namespace az204quizmasterAPI.Services
{
    public class JsonIntakeService
    {
        private DataContext _context;
        public JsonIntakeService(DataContext context)
        {
            _context = context;
        }

        public string[] IngestJson(JsonIntake[] jsonIntakes)
        {
            if (jsonIntakes == null || jsonIntakes.Length == 0)
            {
                return ["error parsing Json"];
            }

            string[] errors = ValidateJson(jsonIntakes);

            if (errors.Length == 0)
            {
                ParseAndSaveJsonIntakes(jsonIntakes);
            }

            return errors;
        }

        private string[] ValidateJson(JsonIntake[] jsonIntakes)
        {
            string[] errors = [];

            foreach(var jsonIntake in jsonIntakes)
            {
                if (jsonIntake.OptionIntakes.Count < 2)
                {
                    errors.Append("Question [" + jsonIntake.Question + "] must have at least 2 options.");
                }

                switch (jsonIntake.QuestionType)
                {
                    case QuestionTypeEnum.MultipleChoiceSingle :
                        errors.Append(ValidateMultipleChoiceSingle(jsonIntake));
                        break;
                    case QuestionTypeEnum.MultipleChoiceMultiple:
                        errors.Append(ValidateMultipleChoiceMulitple(jsonIntake));
                        break;
                    case QuestionTypeEnum.Match:
                        errors.Append(ValidateMatch(jsonIntake));
                        break;
                    default:
                        return ["Invalid QuestionType"];
                }
            }

            return errors;
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

        private void ParseAndSaveJsonIntakes(JsonIntake[] jsonIntakes)
        {
            foreach(var jsonIntake in jsonIntakes)
            {
                QA qa = QABuilder.BuildQA(jsonIntake);
                _context.Add(qa);

                foreach(var option in qa.Options)
                {
                    _context.Add(option);
                }

                _context.SaveChanges();
            }
        }
    }
}
