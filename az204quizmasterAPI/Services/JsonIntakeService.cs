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

        public List<string> IngestJson(JsonIntake[] jsonIntakes)
        {
            if (jsonIntakes == null || jsonIntakes.Length == 0)
            {
                return ["error parsing Json"];
            }

            List<string> errors = ValidateJson(jsonIntakes);

            if (errors.Count == 0)
            {
                errors = ParseAndSaveJsonIntakes(jsonIntakes);
            }

            return errors;
        }

        private List<string> ValidateJson(JsonIntake[] jsonIntakes)
        {
            List<string> errors = new List<string>();

            foreach (var jsonIntake in jsonIntakes)
            {
                if (jsonIntake.Options.Count < 2)
                {
                    errors.Add("Question [" + jsonIntake.Question + "] must have at least 2 options.");
                }

                if (jsonIntake.Question == null)
                {
                    errors.Add("Question field must not be null.");
                }

                string? validationError = null;

                switch (jsonIntake.QuestionType)
                {
                    case QuestionTypeEnum.MultipleChoiceSingle:
                        validationError = ValidateMultipleChoiceSingle(jsonIntake);
                        break;
                    case QuestionTypeEnum.MultipleChoiceMultiple:
                        validationError = ValidateMultipleChoiceMulitple(jsonIntake);
                        break;
                    case QuestionTypeEnum.Match:
                        validationError =ValidateMatch(jsonIntake);
                        break;
                    default:
                        return ["Invalid QuestionType"];
                }

                if (validationError != null)
                {
                    errors.Add(validationError);
                }
            }

            return errors;
        }

        private string? ValidateMultipleChoiceSingle(JsonIntake jsonIntake)
        {
            int correctAnswerCount = GetCorrectAnswerCount(jsonIntake.Options);
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
            int correctAnswerCount = GetCorrectAnswerCount(jsonIntake.Options);
            if (correctAnswerCount == 0)
            {
                return "Question [" + jsonIntake.Question + "] must have at least one answer.";
            }
            return null;
        }

        private string? ValidateMatch(JsonIntake jsonIntake)
        {
            bool hasValidAnswers = true;
            foreach (OptionIntake option in jsonIntake.Options)
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

        private List<string> ParseAndSaveJsonIntakes(JsonIntake[] jsonIntakes)
        {
            List<string> errors = new List<string>();

            foreach (var jsonIntake in jsonIntakes)
            {
                QA qa = QABuilder.BuildQA(jsonIntake);

                QA? foundQa = _context.QAs.Where(q => q.Question == jsonIntake.Question).FirstOrDefault();

                if (foundQa == null)
                {
                    _context.Add(qa);

                    foreach (var option in qa.Options)
                    {
                        _context.Add(option);
                    }

                    foreach (var link in qa.Links)
                    {
                        _context.Add(link);
                    }

                    _context.SaveChanges();
                } 
                else
                {
                    Console.WriteLine("Duplicate question detected: " + jsonIntake.Question);
                    errors.Add("Duplicate question detected: " + jsonIntake.Question);
                }
            }
            return errors;
        }
    }
}
