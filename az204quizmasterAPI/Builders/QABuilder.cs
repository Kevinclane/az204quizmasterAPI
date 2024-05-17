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
                jsonIntake.Image,
                jsonIntake.Category
            );

            ICollection<Option> options = [];
            foreach (OptionIntake optionIntake in jsonIntake.Options)
            {
                options.Add(OptionBuilder.BuildOption(optionIntake, qa));
            }
            qa.Options = options;

            ICollection<ResourceLink> links = [];
            foreach (string link in jsonIntake.ResourceLinks)
            {
                ResourceLink resourceLink = new ResourceLink(link);
                resourceLink.QA = qa;
                links.Add(resourceLink);
            }
            qa.Links = links;

            return qa;
        }
    }
}
