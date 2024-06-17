using az204quizmasterAPI.Models.Entities;

namespace az204quizmasterAPI.Models.RequestModels
{
    public class QuizRequest
    {
        public bool Compute { get; set; }
        public bool Storage { get; set; }
        public bool Security { get; set; }
        public bool Monitor { get; set; }
        public bool ThirdParty { get; set; }

        public bool isValid()
        {
            if (!Compute && !Storage && Security && !Monitor && !ThirdParty)
            {
                return false;
            }
            return true;
        }
        
    }
}
