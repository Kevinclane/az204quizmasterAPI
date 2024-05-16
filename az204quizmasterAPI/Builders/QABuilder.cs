using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.RequestModels;

namespace az204quizmasterAPI.Builders
{
    public class QABuilder {
        public static QA BuildQA(JsonIntake jsonIntake)
        {
            QA qa = new QA(
                jsonIntake.Question,
                jsonIntake.QuestionType,
                jsonIntake.ResourceLink,
                jsonIntake.Image,
                jsonIntake.Category
            );

            ICollection<Option> options = [];

            foreach (OptionIntake optionIntake in jsonIntake.OptionIntakes)
            {
                options.Add(OptionBuilder.BuildOption(optionIntake, qa));
            }

            qa.Options = options;

            return qa;
        }
    }
}
