namespace az204quizmasterAPI.Models.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public int QAId { get; set; }
        public required string LeftDisplay { get; set; }
        public string? RightDisplay { get; set; }
        public bool IsCorrect { get; set; }
    }
}
