namespace az204quizmasterAPI.Models.Entities
{
    public class Quiz
    {
        public Quiz()
        {
            ActiveQAs = new List<ActiveQA>();
        }

        public int Id { get; set; }

        public ICollection<ActiveQA> ActiveQAs { get; set; }
    }
}
