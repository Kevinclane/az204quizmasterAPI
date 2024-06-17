using az204quizmasterAPI.Models.Entities;

namespace az204quizmasterAPI.Models.ViewModels
{
    public class OptionVM
    {
        public int Id { get; set; }
        public string LeftDisplay { get; set; }
        public string? RightDisplay { get; set; }

        public OptionVM(Option option)
        {
            Id = option.Id;
            LeftDisplay = option.LeftDisplay;
            RightDisplay = option.RightDisplay;
        }
    }
}
