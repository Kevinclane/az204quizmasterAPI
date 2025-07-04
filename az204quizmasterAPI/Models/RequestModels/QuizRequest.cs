using az204quizmasterAPI.Models.Entities;
using az204quizmasterAPI.Models.Enums;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class QuizRequest
    {
        public bool Compute { get; set; }
        public bool Storage { get; set; }
        public bool Security { get; set; }
        public bool Monitor { get; set; }
        public bool ThirdParty { get; set; }
        public int QuestionCount { get; set; }

        public bool isValid()
        {
            if (!Compute && !Storage && Security && !Monitor && !ThirdParty)
            {
                return false;
            }
            return true;
        }

        public List<int> getCategories()
        {
            List<int> list = new List<int>();

            if (Compute)
            {
                list.Add((int)CategoryEnum.ComputeSolutions);
            }

            if (Storage)
            {
                list.Add((int)CategoryEnum.Storage);
            }

            if (Security)
            {
                list.Add((int)CategoryEnum.Security);
            }

            if (Monitor)
            {
                list.Add((int)CategoryEnum.Monitor);
            }

            if (ThirdParty)
            {
                list.Add((int)CategoryEnum.ThirdParty);
            }

            return list;
        }
        
    }
}
